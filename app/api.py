import json
import logging
import time
from elasticsearch import Elasticsearch
from flask import Blueprint, Response, request, jsonify
from flask_jwt_extended import create_access_token, jwt_required, get_jwt_identity
from werkzeug.security import check_password_hash
from .models import db, User
from .utils import search_known_issues, get_embedding
from . import redis_client
from datetime import timedelta
from .config import Config

logger = logging.getLogger()

es = Elasticsearch(Config.ELASTICSEARCH_URL)

api_bp = Blueprint('api', __name__)

@api_bp.route('/register', methods=['POST'])
def register_user():
    logging.info("Register endpoint accessed")
    data = request.get_json()
    username = data.get('username')
    password = data.get('password')

    if not username or not password:
        logging.warning("Missing username or password in registration request")
        return jsonify({"msg": "Missing username or password"}), 400

    # Check if the user already exists
    user = User.query.filter_by(username=username).first()
    if user:
        logging.warning(f"User registration failed: User '{username}' already exists")
        return jsonify({"msg": "User already exists"}), 400

    new_user = User(username=username)
    new_user.set_password(password)
    db.session.add(new_user)
    db.session.commit()

    logging.info(f"User '{username}' registered successfully")
    return jsonify({"msg": "User registered successfully"}), 201

@api_bp.route('/login', methods=['POST'])
def login_user():
    logging.info("Login endpoint accessed")
    data = request.get_json()
    username = data.get('username')
    password = data.get('password')

    user = User.query.filter_by(username=username).first()
    if not user or not user.check_password(password):
        logging.warning(f"Invalid login attempt for username '{username}'")
        return jsonify({"msg": "Invalid credentials"}), 401

    # Generate JWT token
    access_token = create_access_token(identity=username, expires_delta=timedelta(seconds=Config.JWT_EXPIRATION))
    logging.info(f"User '{username}' logged in successfully")
    return jsonify(access_token=access_token)




@api_bp.route('/search', methods=['POST'])
@jwt_required()
def get_ai_response():
    """Classify a user query with caching and rate limiting."""
    logging.info("Search endpoint accessed")
    user_identity = get_jwt_identity()
    rate_limit_key = f"rate_limit:{user_identity}"
    logging.debug(f"Rate limiting key: {rate_limit_key}")

    start_time = time.time()  # Start tracking response time

    # Get current count of requests made in the rate-limit window
    current_count = redis_client.get(rate_limit_key) if Config.USE_REDIS else None
    if current_count:
        logging.debug(f"Current request count for user '{user_identity}': {current_count.decode()}")

    # Check rate limit
    if Config.USE_REDIS and current_count and int(current_count) >= Config.RATE_LIMIT_MAX_REQUESTS:
        logging.warning(f"Rate limit exceeded for user '{user_identity}'")
        return jsonify({"msg": "Rate limit exceeded, try again later."}), 429

    # Retrieve the user query
    data = request.get_json()
    query = data.get("query")
    if not query:
        logging.warning("Search request missing 'query' parameter")
        return jsonify({"msg": "Query is required"}), 400

    # Check Redis cache for a stored result
    if Config.USE_REDIS:
        cached_result = redis_client.get(query)
        if cached_result:
            elapsed_time = round(time.time() - start_time, 2)
            logging.info(f"Cache hit for query: '{query}'")
            decoded_result = json.loads(cached_result)
            return jsonify({
                "response": decoded_result,
                "cached": "true",
                "time": elapsed_time
            })

    # If not cached, query Elasticsearch and classify
    try:
        # Unpack 3 values from search_known_issues: result, cache hit, and elapsed time
        ai_response, cache_hit, elapsed_time = search_known_issues(query, es)
        #print(json.dumps(ai_response, indent=4))
        solution = ai_response.get("solution")  # Only extract the solution
    except Exception as e:
        logging.error(f"Error during Elasticsearch query: {str(e)}")
        return jsonify({"msg": f"Error getting AI response: {str(e)}"}), 500

    elapsed_time = round(time.time() - start_time, 2)

    # Cache the response in Redis and increment rate limit count
    if Config.USE_REDIS:
    # Increment rate limit count or set it if not present
        if current_count:
            redis_client.incr(rate_limit_key)
            logging.debug(f"Incremented rate limit count for user '{user_identity}'")
        else:
            redis_client.setex(rate_limit_key, Config.RATE_LIMIT_WINDOW_SECONDS, 1)
            logging.debug(f"Rate limit key set for user '{user_identity}' with a window of {Config.RATE_LIMIT_WINDOW_SECONDS} seconds")

        # Cache the AI response only if the solution is not "No solution found"
        if solution != "No feasible solution found":
            redis_client.setex(query, Config.REDIS_CACHE_EXPIRATION, json.dumps(ai_response, ensure_ascii=False))
            logging.info(f"Cached response for query: '{query}' with expiration {Config.REDIS_CACHE_EXPIRATION} seconds")
        else:
            logging.info(f"No feasible solution found for query: '{query}'. Response not cached.")

    # Return only the solution in the response
    response = {
        "response": {"solution": solution},
        "cached": "false" if solution == "No feasible solution found" else "true",
        "time": elapsed_time
    }

    logging.info(f"Returning response for query: '{query}'")
    return Response(json.dumps(response, ensure_ascii=False), mimetype='application/json; charset=utf-8')



@api_bp.route('/memory', methods=['POST'])
@jwt_required()
def store_memory():
    """Store a new document with title, issue, solution, category, custom fields, and embedding."""
    logging.info("Memory endpoint accessed")

    # Get the data from the request
    try:
        data = request.get_json()
        logging.debug(f"Received data: {data}")  # Debug message to print the received data
    except Exception as e:
        logging.error(f"Error parsing JSON data: {str(e)}")
        return jsonify({"msg": "Error parsing JSON data"}), 400

    title = data.get("Title")
    issue = data.get("Issue")
    solution = data.get("Solution")
    category = data.get("Category")
    custom_fields = data.get("CustomFields")  # Expected to be a dictionary or null

    # Validate the required fields
    if not title or not issue or not solution or not category:
        logging.warning("Missing required fields in memory request")
        return jsonify({"msg": "Title, Issue, Solution, and Category are required"}), 400
    
    # Debug message to show the fields received
    logging.debug(f"Title: {title}, Issue: {issue}, Solution: {solution}, Category: {category}")

    # Generate embedding for the combined title, issue, and solution
    combined_text = f"{title} {issue} {solution}"
    logging.debug(f"Combined text for embedding: {combined_text}")  # Debug the combined text
    combined_embedding = get_embedding(combined_text)

    if not combined_embedding or len(combined_embedding) != 768:
        logging.error("Failed to generate valid embedding for the combined title, issue, and solution")
        return jsonify({"msg": "Error generating embedding for the combined title, issue, and solution"}), 500

    # Optionally, generate a hash for the document (using title and issue as an example)
    document_hash = f"{title}_{issue}"
    logging.debug(f"Generated document hash: {document_hash}")  # Debug the document hash
    
    # Document structure to be indexed in Elasticsearch
    document = {
        "title": title,
        "issue": issue,
        "solution": solution,
        "category": category,
        "customFields": custom_fields,  # This could be a dictionary or null
        "hash": document_hash,  # You can use any method to generate the hash
        "embedding": combined_embedding
    }

    try:
        # Index the document in Elasticsearch
        es.index(index="issues_n_solutions", document=document)
        logging.info("Document indexed successfully")
        return jsonify({"msg": "Document stored successfully"}), 201
    except Exception as e:
        logging.error(f"Error indexing document: {str(e)}")
        return jsonify({"msg": f"Error storing document: {str(e)}"}), 500
