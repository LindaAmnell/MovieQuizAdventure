namespace MovieQuizAdventure.Models
{
    public class QuizGame
    {

        private Quiz quiz;
        private List<Question> questions;
        private int index = 0;

        public int CorrectAnswers { get; private set; }
        public int TotalAnswered { get; private set; }




        public QuizGame(Quiz qu)
        {
            quiz = qu;
            questions = quiz.Questions.OrderBy(q => Guid.NewGuid()).ToList();
        }

        public bool CheckAnswer(Question question, int selectedAnswer)
        {
            TotalAnswered++;

            if (question.CorrectAnswer == selectedAnswer)
            {
                CorrectAnswers++;
                return true;
            }

            return false;
        }

        public Question GetNextQuestion()
        {
            if (index >= questions.Count)
            {
                return null;
            }
            return questions[index++];
        }

        public double GetScorePercent()
        {
            if (TotalAnswered == 0)
            {
                return 0;
            }

            return (double)CorrectAnswers / TotalAnswered * 100;
        }

        public bool IsDone => index >= questions.Count;
    }
}
