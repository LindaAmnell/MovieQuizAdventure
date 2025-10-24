namespace MovieQuizAdventure.Models
{
    public class Quiz
    {
        public string Title { get; set; }

        public List<Question> questions { get; set; }

        public Random Randomizer { get; set; }

        public Quiz(string title = "")
        {
            Title = title;
            questions = new List<Question>();
            Randomizer = new Random();
        }

        public Question GetRandomQuestion()
        {
            var newQuestionList = questions.ToList();
            int index = Randomizer.Next(0, newQuestionList.Count);
            return questions[index];
        }

        public void AddQuestion(string statement, int correctAnswer, params string[] answers)
        {
            var newQuestion = new Question(statement, answers, correctAnswer);
            questions.Add(newQuestion);

        }

        public void RemoveQuestion(int index)
        {
            if (index < 0 || index >= questions.Count)
            {
                return;
            }
            questions.RemoveAt(index);
        }

        public int QuestionCount => questions.Count;

    }
}
