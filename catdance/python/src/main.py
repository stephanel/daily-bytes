from flask import Flask, render_template
import os
import random
from stores import images
  
app = Flask(__name__)

@app.route('/')
@app.route('/index')
def index():
    url = random.choice(images)
    return render_template("index.html", url=url)

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=int(os.environ.get("PORT", 5000)))
