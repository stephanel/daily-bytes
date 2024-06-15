## Run application
Enter the following command in a terminal.

```bash
pip install --user pipenv
pipenv install flask
pipenv run python ./src/main.py
```
or

```bash
pip install flask
export FLASK_APP=./src/main.py
flask run
```

Then navigate to http://localhost:5000.