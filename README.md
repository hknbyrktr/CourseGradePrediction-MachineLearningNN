About the Project
This project is built by combining the power of Python and Unity technologies.
The main goal is to collect input from the user via a Unity-based application, send this data to neural network models trained with Python, and display the model's output back to the user through the Unity interface.

🚀 Unity (Application Interface)
The Unity side handles the user interaction and interface.
It collects input from the user and sends it to the machine learning models developed in Python.
The model’s prediction is then displayed to the user through the Unity interface.

🧠 Python (Model Training & Prediction)
There are three main Python scripts used in this project:

📘 GradeAndSectionPrediction.ipynb
This Jupyter notebook covers the data preparation and analysis process before model training:

Data Preprocessing

Exploratory Data Analysis (EDA)

Regression Models and R² Comparison

(For presentation purposes only:) Binary Classification

F1 Score, Precision, Recall, Accuracy metrics comparison

Confusion Matrix and ROC Curve visualizations

Explainable AI (XAI) - SHAP

Note: The classification tasks in this notebook were included solely for course presentation purposes. The main focus of the project is on regression models.

📘 GanoModelCreator.py
This script creates regression models that predict GPA (General Weighted Average) for each of the three departments in the dataset.

Note: Although the number of departments is limited, it is sufficient for foundational modeling.

📘 LessonsModelCreator.py
This script builds models that predict the scores of courses for the department selected by the user.

Note: Each department includes only 10 courses in the dataset (kept limited for demonstration purposes).

