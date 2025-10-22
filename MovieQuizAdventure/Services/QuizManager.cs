using MovieQuizAdventure.Models;


namespace MovieQuizAdventure.Services
{
    public class QuizManager
    {

        private Quiz quiz;

        public QuizManager()
        {
            quiz = null;
        }

        public QuizManager(Quiz existingQuiz)
        {
            quiz = existingQuiz;
        }


        public Quiz NewQuiz(string title)
        {
            quiz = new Quiz(title);

            return quiz;
        }

        public bool NewQuestion(string statement, string[] answers, int correctAnswer)
        {

            if (quiz == null)
            {
                return false;
            }
            quiz.AddQuestion(statement, correctAnswer, answers);
            return true;
        }

        public Question EditQuestion(int index, string newState, string[] newAnswer, int newCorrectAnswer)
        {

            if (quiz == null)
            {
                return null;
            }

            if (index < 0 || index >= quiz.Questions.Count())
            {
                return null;
            }
            var updatedQuestion = new Question(newState, newAnswer, newCorrectAnswer);

            quiz.RemoveQuestion(index);
            quiz.AddQuestion(newState, newCorrectAnswer, newAnswer);

            return updatedQuestion;
        }

        public bool RenameQuiz(string newTitle)
        {
            if (quiz == null)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(newTitle))
            {
                return false;
            }

            quiz.SetTitle(newTitle);

            return true;
        }

        public Quiz GetCurrentQuiz(int index)
        {

            if (quiz == null)
            {
                return null;
            }

            return quiz;
        }

    }
}
