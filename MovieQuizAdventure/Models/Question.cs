namespace MovieQuizAdventure.Models
{
    public class Question
    {
        public string Statement { get; }
        public string[] Answers { get; }
        public int CorrectAnswer { get; }
    }
}
