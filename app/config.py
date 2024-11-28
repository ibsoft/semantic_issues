import os


class Config:
    # Secret key for JWT
    SECRET_KEY = os.getenv("SECRET_KEY", "prod-sjdgfiwa73468wyfgsjkghfr7w7raksjfd")
    JWT_EXPIRATION = 360000  # 1 hour
    RATE_LIMIT_MAX_REQUESTS = 10000  # Maximum requests per time window
    RATE_LIMIT_WINDOW_SECONDS = 1  # Time window in seconds (5 minutes)
    OPENAI_API_KEY = os.getenv("OPENAI_API_KEY")
    OLLAMA_API_URL = "http://172.17.0.1:11434/api/embeddings"
    OLLAMA_MODEL = "paraphrase-multilingual"
    MODEL = "gpt-4o-mini"  # Set this to your GPT model ID or name
    LOG_LEVEL = os.getenv("LOG_LEVEL", "INFO")
    # Redis URL for caching
    REDIS_URL = "redis://172.17.0.1:6379/0"
    BASE_DIR = os.path.abspath(os.path.dirname(__file__))
    SQLALCHEMY_DATABASE_URI = f"sqlite:///{os.path.join(BASE_DIR, 'database/app.db')}"
    SQLALCHEMY_TRACK_MODIFICATIONS = False
    ELASTICSEARCH_URL = "http://172.17.0.1:9200"
    USE_REDIS = True  # Check if Redis caching is enabled
    REDIS_CACHE_EXPIRATION = 3600  # Cache expiration in seconds
