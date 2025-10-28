using MovieQuizAdventure.Models;
using System.Collections.ObjectModel;

namespace MovieQuizAdventure.Services
{
    public class QuizManager
    {
        public static QuizManager Instance { get; } = new QuizManager();
        public ObservableCollection<Quiz> quizzes { get; private set; }

        private QuizManager()
        {
            JsonStorage.EnsureDefaultQuizzesFromProject();
            quizzes = new ObservableCollection<Quiz>();
            _ = LoadAllSavedQuizzes();
        }

        public ObservableCollection<Quiz> GetAllQuizzes()
        {
            return quizzes;
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
    }
}
