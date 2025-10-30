using System.Text.Json.Serialization;

namespace MovieQuizAdventure.Models
{
    public class Question
    {
        public string Statement { get; set; }
        public string[] Answers { get; set; }
        public int CorrectAnswer { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MovieCategory Category { get; set; }

        public string? ImageUrl { get; set; }

        public Question(string statement, string[] answers, int correctAnswer, MovieCategory category)
        {
            Statement = statement;
            Answers = answers;
            CorrectAnswer = correctAnswer;
            Category = category;
        }
        public bool IsCorrect(int selectedIndex)
        {
            return selectedIndex == CorrectAnswer;
        }
    }
}
