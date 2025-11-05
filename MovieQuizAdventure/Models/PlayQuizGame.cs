using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MovieQuizAdventure.Models
{
    public class PlayQuizGame : INotifyPropertyChanged
    {
        public Quiz Quiz { get; set; }
        public Question CurrentQuestion { get; set; }
        public int SelectedAnswerIndex { get; set; }
        public int CorrectAnswers { get; set; }
        public int TotalAnswered { get; set; }

        private List<Question> usedQuestions = new();
        public string? CurrentImageUrl => CurrentQuestion?.ImageUrl;

        public ImageSource CurrentImageSource
        {
            get
            {
                if (string.IsNullOrEmpty(CurrentImageUrl))
                    return null;
                try
                {
                    if (CurrentImageUrl.StartsWith("data:image"))
                    {
                        var base64 = CurrentImageUrl.Substring(CurrentImageUrl.IndexOf(",") + 1);
                        var bytes = Convert.FromBase64String(base64);

                        using (var ms = new MemoryStream(bytes))
                        {
                            var img = new BitmapImage();
                            img.BeginInit();
                            img.StreamSource = ms;
                            img.CacheOption = BitmapCacheOption.OnLoad;
                            img.EndInit();
                            img.Freeze();
                            return img;
                        }
                    }
                    return new BitmapImage(new Uri(CurrentImageUrl));
                }
                catch
                {
                    return null;
                }
            }
        }


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
                OnPropertyChanged(nameof(CurrentImageSource));
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
            OnPropertyChanged(nameof(CurrentImageSource));
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
