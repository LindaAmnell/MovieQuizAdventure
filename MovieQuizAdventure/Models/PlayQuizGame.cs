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
        public PlayQuizGame(Quiz quiz)
        {
            Quiz = quiz;
            CurrentQuestion = Quiz.GetRandomQuestion();
            OnPropertyChanged("CurrentQuestion");
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
            OnPropertyChanged(nameof(ScoreText));
        }

        //public bool IsDone => index >= Quiz.Count;
    }
}
