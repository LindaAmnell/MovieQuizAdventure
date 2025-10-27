namespace MovieQuizAdventure.Models
{
    public class Question
    {
        public string Statement { get; set; }
        public string[] Answers { get; set; }
        public int CorrectAnswer { get; set; }

        public Question(string statement, string[] answers, int correctAnswer)
        {
            Statement = statement;
            Answers = answers;
            CorrectAnswer = correctAnswer;
        }

        public bool IsCorrect(int selectedIndex)
        {
            return selectedIndex == CorrectAnswer;
        }


    }
}
