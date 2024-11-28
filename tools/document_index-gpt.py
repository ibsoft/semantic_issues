import json
import hashlib
from elasticsearch import Elasticsearch
import openai

# Elasticsearch configuration
es_host = "http://192.168.7.10:9200"  # Replace with your Elasticsearch host
index_name = "issues_n_solutions"

# OpenAI API Key
openai.api_key = "your_open_ai_api_key"

# Initialize Elasticsearch client
es = Elasticsearch([es_host])

# Create Elasticsearch index with dynamic mapping for custom fields
def create_index_if_not_exists():
    mapping = {
        "mappings": {
            "properties": {
                "title": {"type": "text"},
                "issue": {"type": "text"},
                "solution": {"type": "text"},
                "category": {"type": "keyword"},
                "custom_fields": {"type": "object"},
                "hash": {"type": "keyword"},
                "embedding": {
                    "type": "dense_vector",
                    "dims": 1536  # Matches the embedding model's dimensions
                }
            }
        }
    }

    if not es.indices.exists(index=index_name):
        es.indices.create(index=index_name, body=mapping)
        print(f"Created Elasticsearch index: {index_name}")
    else:
        print(f"Elasticsearch index {index_name} already exists.")

# Generate a hash for deduplication
def generate_document_hash(doc):
    hash_source = f"{doc['title']}{doc['issue']}{doc['solution']}"
    return hashlib.md5(hash_source.encode()).hexdigest()

# Check if a document is unchanged
def is_document_unchanged(doc):
    doc_hash = generate_document_hash(doc)
    search_query = {
        "query": {
            "term": {
                "hash": doc_hash
            }
        }
    }

    try:
        response = es.search(index=index_name, body=search_query)
        return response['hits']['total']['value'] > 0
    except Exception as e:
        print(f"Error checking document hash in Elasticsearch: {e}")
        return False

# Generate embeddings using OpenAI's API
def get_embedding(text):
    try:
        response = openai.Embedding.create(
            model="text-embedding-ada-002",
            input=text
        )
        return response['data'][0]['embedding']
    except Exception as e:
        print(f"Error generating embedding for text: {text[:30]}...: {e}")
        return None

# Index a document into Elasticsearch
def index_document_with_embedding(doc):
    # Combine fields for embedding
    embedding_text = f"{doc['title']} {doc['issue']} {doc['solution']}"
    embedding = get_embedding(embedding_text)
    if embedding is None:
        print(f"Failed to generate embedding for document: {doc['title']}")
        return

    doc_with_embedding = {
        "title": doc["title"],
        "issue": doc["issue"],
        "solution": doc["solution"],
        "category": doc["category"],
        "custom_fields": doc.get("custom_fields", {}),
        "hash": generate_document_hash(doc),
        "embedding": embedding
    }

    try:
        es.index(index=index_name, body=doc_with_embedding)
        print(f"Successfully indexed document: {doc['title']}")
    except Exception as e:
        print(f"Error indexing document {doc['title']}: {e}")

# Process documents from JSON file and index them
def process_documents_on_startup():
    try:
        with open("issues_n_solutions.json", "r", encoding="utf-8") as file:
            documents = json.load(file)
            for doc in documents:
                try:
                    if not is_document_unchanged(doc):
                        index_document_with_embedding(doc)
                        print(f"Indexed document: {doc['title']}")
                    else:
                        print(f"Document unchanged, skipping: {doc['title']}")
                except Exception as e:
                    print(f"Error processing document {doc['title']}: {e}")
    except FileNotFoundError:
        print("issues_n_solutions.json file not found. No documents indexed.")
    except Exception as e:
        print(f"Error reading issues_n_solutions.json: {e}")

# Main process
if __name__ == "__main__":
    create_index_if_not_exists()
    process_documents_on_startup()
