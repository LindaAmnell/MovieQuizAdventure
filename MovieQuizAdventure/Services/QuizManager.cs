using MovieQuizAdventure.Models;
using System.Collections.ObjectModel;

namespace MovieQuizAdventure.Services
{
    public class QuizManager
    {
        public ObservableCollection<Quiz> quizzes = new();
        private Quiz currentQuiz;

        public ObservableCollection<Quiz> GetAllQuizzes()
        {
            return quizzes;
        }
        public QuizManager()
        {
            quizzes = new ObservableCollection<Quiz>();
            _ = LoadAllSavedQuizzes();
        }

        public Quiz CreateQuiz(string title)
        {
            var newQuiz = new Quiz(title);
            quizzes.Add(newQuiz);
            currentQuiz = newQuiz;
            return newQuiz;
        }

        public async Task LoadAllSavedQuizzes()
        {
            var savedQuizNames = JsonStorage.GetSavedQuizFiles();

            foreach (var quizName in savedQuizNames)
            {
                var loadedQuiz = await JsonStorage.LoadQuizAsync(quizName);
                if (loadedQuiz != null)
                {
                    quizzes.Add(loadedQuiz);
                }
            }
        }
        public List<string> GetSavedQuizzes()
        {
            return JsonStorage.GetSavedQuizFiles();
        }

        public Quiz GetQuizByTitle(string title)
        {
            return quizzes.FirstOrDefault(q => q.Title == title);
        }

        public void SetCurrentQuiz(Quiz quiz)
        {
            currentQuiz = quiz;
        }

        public Quiz GetCurrentQuiz()
        {
            return currentQuiz;
        }
    }
}
