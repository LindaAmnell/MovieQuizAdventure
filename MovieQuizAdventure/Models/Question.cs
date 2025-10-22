namespace MovieQuizAdventure.Models
{
    public class Question
    {
        public string Statement { get; }
        public string[] Answers { get; }
        public int CorrectAnswer { get; }

        public Question(string statement, string[] answers, int correctAnswer)
        {
            Statement = statement;
            Answers = answers;
            CorrectAnswer = correctAnswer;
        }

    }
}
