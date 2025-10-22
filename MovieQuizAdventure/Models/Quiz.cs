namespace MovieQuizAdventure.Models
{
    public class Quiz
    {
        private IEnumerable<Question> _questions;
        private string _title = string.Empty;
        public IEnumerable<Question> Questions => _questions;
        public string Title => _title;

        private readonly List<Question> questionList = new List<Question>();
        private static readonly Random random = new Random();

        public Quiz()
        {
            _questions = new List<Question>();
        }

        public Quiz(string title) : this()
        {
            _title = title;
            _questions = questionList;
        }

        public void SetTitle(string newTitle)
        {
            _title = newTitle;
        }

        public Question GetRandomQuestion()
        {
            if (questionList.Count == 0)
            {
                throw new InvalidOperationException("The quiz contains no questions.");

            }

            int randomIndex = random.Next(questionList.Count);
            return questionList[randomIndex];


        }

        public void AddQuestion(string statement, int correctAnswer, params string[] answers)
        {
            var newQuestion = new Question(statement, answers, correctAnswer);
            questionList.Add(newQuestion);
            _questions = questionList;
        }

        public void RemoveQuestion(int index)
        {

            if (index < 0 || index >= questionList.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }

            questionList.RemoveAt(index);
            _questions = questionList;

        }

        public int QuestionCount => questionList.Count;

    }
}
