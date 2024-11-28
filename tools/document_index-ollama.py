import requests
import json
from elasticsearch import Elasticsearch

# Elasticsearch setup
es = Elasticsearch(["http://192.168.7.10:9200"])

# Ollama API setup
OLLAMA_API_URL = "http://localhost:11434/api/embeddings"
OLLAMA_MODEL = "paraphrase-multilingual"


def get_embedding(text):
    """
    Fetch embedding for the given text using Ollama API.
    """
    headers = {"Content-Type": "application/json"}
    payload = {"model": OLLAMA_MODEL, "prompt": text}
    try:
        response = requests.post(
            OLLAMA_API_URL, headers=headers, json=payload, timeout=30
        )
        response.raise_for_status()
        response_json = response.json()
        return response_json.get("embedding", None)
    except requests.RequestException as e:
        print(f"Request failed: {e}")
        return None
    except (json.decoder.JSONDecodeError, KeyError):
        print("Error decoding JSON response or missing 'embedding' key.")
        return None


def create_index():
    """
    Create an Elasticsearch index with mappings for dense vector and custom fields.
    """
    index_mapping = {
        "mappings": {
            "properties": {
                "title": {"type": "text"},
                "issue": {"type": "text"},
                "solution": {"type": "text"},
                "category": {"type": "keyword"},
                # Ensure dims match embedding size
                "embedding": {"type": "dense_vector", "dims": 768},
                # Allows dynamic fields for custom attributes
                "custom_fields": {"type": "object"},
            }
        }
    }
    # Delete index if it exists (for development purposes; avoid in production)
    es.indices.delete(index="issues_n_solutions", ignore=[400, 404])
    # Create the index with the specified mapping
    es.indices.create(index="issues_n_solutions",
                      body=index_mapping, ignore=400)


def index_documents(data):
    """
    Index documents into Elasticsearch with embeddings and custom fields.
    """
    for doc in data:
        document_id = doc.get("id")
        title = doc.get("title", "")
        issue = doc.get("issue", "")
        solution = doc.get("solution", "")
        category = doc.get("category", "")
        custom_fields = doc.get("custom_fields", {})

        # Concatenate key fields for embedding
        embedding_text = f"{title} {issue} {solution}"
        embedding = get_embedding(embedding_text)

        # Validate embedding size
        if not embedding or len(embedding) != 768:
            print(f"Skipping document {document_id} due to invalid embedding.")
            continue

        # Prepare document for indexing
        document = {
            "title": title,
            "issue": issue,
            "solution": solution,
            "category": category,
            "embedding": embedding,
            "custom_fields": custom_fields,
        }

        try:
            es.index(index="issues_n_solutions",
                     id=document_id, document=document)
            print(f"Document {document_id} indexed successfully.")
        except Exception as e:
            print(f"Failed to index document {document_id}: {e}")


if __name__ == "__main__":
    # Load dataset from JSON file
    try:
        with open("issues_n_solutions.json", "r", encoding="utf-8") as f:
            dataset = json.load(f)
    except FileNotFoundError:
        print("The file 'issues_n_solutions.json' was not found.")
        exit(1)
    except json.JSONDecodeError:
        print("Error decoding JSON from 'issues_n_solutions.json'.")
        exit(1)

    # Create Elasticsearch index and index documents
    create_index()
    index_documents(dataset)
    print("All documents indexed successfully!")
