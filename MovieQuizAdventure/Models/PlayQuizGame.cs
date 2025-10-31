using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MovieQuizAdventure.Models
{
    public class PlayQuizGame : INotifyPropertyChanged
    {
        private int index = 0;
        public Quiz Quiz { get; set; }
        public Question CurrentQuestion { get; set; }
        public int SelectedAnswerIndex { get; set; }
        public int CorrectAnswers { get; set; }
        public int TotalAnswered { get; set; }

        private List<Question> usedQuestions = new();
        public string? CurrentImageUrl => CurrentQuestion?.ImageUrl;

        public string ScoreText
        {
            get
            {
                int percent = 0;
                if (TotalAnswered > 0)
                {
                    percent = (int)((double)CorrectAnswers / TotalAnswered * 100);
                }
                return $"Score:  {CorrectAnswers} / {TotalAnswered}    ({percent}%)";
            }
        }
        public string QuestionProgressText
        {
            get
            {
                return $"Question: {usedQuestions.Count + 1} / {Quiz.QuestionCount}";
            }
        }
        public PlayQuizGame(Quiz quiz)
        {
            Quiz = quiz;
            CurrentQuestion = Quiz.GetRandomQuestion();
            OnPropertyChanged(nameof(CurrentQuestion));
            OnPropertyChanged(nameof(CurrentImageUrl));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void GetNextQuestion(int selectedIndex = -1)
        {
            if (CurrentQuestion != null && selectedIndex != -1)
            {
                TotalAnswered++;

                if (CurrentQuestion.CorrectAnswer == selectedIndex)
                    CorrectAnswers++;
                usedQuestions.Add(CurrentQuestion);
            }

            if (usedQuestions.Count >= Quiz.questions.Count)
            {
                CurrentQuestion = null;
                OnPropertyChanged(nameof(CurrentQuestion));
                OnPropertyChanged(nameof(CurrentImageUrl));
                return;
            }
            Question nextQuestion;
            do
            {
                nextQuestion = Quiz.GetRandomQuestion();
            }
            while (usedQuestions.Contains(nextQuestion));
            CurrentQuestion = nextQuestion;
            OnPropertyChanged(nameof(CurrentQuestion));
            OnPropertyChanged(nameof(CurrentImageUrl));
            OnPropertyChanged(nameof(ScoreText));
            OnPropertyChanged(nameof(QuestionProgressText));

        }
        public static Quiz CreateQuizFromCategory(List<Quiz> allQuizzes, MovieCategory category)
        {
            var questions = allQuizzes
                .SelectMany(q => q.questions)
                .Where(q => q.Category == category)
                .ToList();

            if (questions.Count == 0)
                return null;

            return new Quiz($"Category: {category}")
            {
                questions = questions,
                Randomizer = new Random()
            };
        }

    }
}
