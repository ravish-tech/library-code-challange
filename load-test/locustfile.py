import time
import random
from locust import HttpUser, task, between

search_phrases = ["bob", "wel", "call", "stop", "lost"]

class QuickstartUser(HttpUser):
    host = "http://localhost:5123"
    book_id = None
#     wait_time = between(1, 2)
    @task
    def get_books(self):
        res = self.client.get("/api/Books")
        book_ids = list(res.json().keys())
        self.book_id = random.choice(book_ids)

    @task
    def get_words(self):
        if self.book_id:
            self.client.get(f"/api/Books/{self.book_id}")
        
    @task
    def get_words(self):
        if self.book_id:
            search_phrase = random.choice(search_phrases)
            self.client.get(f"/api/Books/{self.book_id}?query={search_phrase}")