# 🎬 Movie Quiz Adventure

<p align="center">
  <img src="https://github.com/user-attachments/assets/095e1131-4504-4100-bebd-047186b2d072" alt="Quiz Screenshot" width="500">
</p>


## 📖 About the Project
This project was developed as part of **Lab 3 – Quiz Game**, where the goal was to build a graphical quiz application using C# (WPF).

The application allows the user to:
- ▶️ **Play a quiz**
- ✏️ **Edit an existing quiz**
- ➕ **Create a brand-new quiz**

Each feature (play, edit, create) is presented in its own view using separate **UserControls**.

## ✅ Features
- Multiple-choice questions (3 answer options per question)
- Questions appear in a **random order**
- After each answer, the user can see:
  - total number of correct answers
  - current success percentage
- Create quiz: name the quiz, add questions, choose which answer is correct
- Edit quiz: display all questions and modify any of them
- Quizzes can be saved in **JSON / XML / CSV format**

## 🧠 Technical Requirements (Met)
The project fulfills all requirements from the assignment:

| Requirement | Completed |
|------------|----------|
| Separate views implemented via UserControls | ✅ |
| Async saving and loading of files | ✅ |
| Quiz and Question classes have proper constructors | ✅ |
| No crashes during runtime / stable application | ✅ |

## ⭐ Extra / Improvements
*(Add your own notes here if you implemented optional features such as images or category filtering.)*

## 🚀 How to Run the Project
1. Open the project in Visual Studio
2. Run the application (`F5`)
3. Choose one of the menu options: *Play Quiz*, *Edit Quiz*, or *Create New Quiz*

## 📂 File Handling
All quizzes are automatically stored in:

AppData/Local/MovieQuizAdventure/
