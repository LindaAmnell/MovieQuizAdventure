using MovieQuizAdventure.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MovieQuizAdventure.Services
{
    public class EditAndCreatQuizViewModel : INotifyPropertyChanged
    {

        private Quiz currentQuiz;
        private Question currentQuestion;
        public Quiz CurrentQuiz => currentQuiz;
        public string Statement { get; set; }
        public string[] Answers { get; set; } = new string[4];
        public int CorrectAnswer { get; set; }
        public MovieCategory Category { get; set; }
        public MovieCategory[] MovieCategoriesList => Enum.GetValues(typeof(MovieCategory))
                                                           .Cast<MovieCategory>()
                                                           .ToArray();
        private bool _useImage;
        public bool UseImage
        {
            get => _useImage;
            set { _useImage = value; OnPropertyChanged(); }
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get => _imageUrl;
            set { _imageUrl = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Question> Questions { get; private set; } = new();

        public int SelectedQuestionIndex { get; set; } = -1;

        private string _quizTitle;
        public string QuizTitle
        {
            get => _quizTitle;
            set
            {
                if (_quizTitle != value)
                {
                    _quizTitle = value;
                    OnPropertyChanged();
                }
            }
        }
        public EditAndCreatQuizViewModel(Question question = null, Quiz quiz = null)
        {
            currentQuiz = quiz;
            currentQuestion = question;

            QuizTitle = currentQuiz?.Title ?? string.Empty;

            if (question != null)
            {
                Statement = question.Statement;
                Answers = question.Answers.ToArray();
                CorrectAnswer = question.CorrectAnswer;
                ImageUrl = question.ImageUrl;
                UseImage = !string.IsNullOrWhiteSpace(ImageUrl);
                Category = question.Category;

            }
            else
            {
                Statement = string.Empty;
                Answers = new string[4];
                CorrectAnswer = 0;
                ImageUrl = string.Empty;
                UseImage = false;
                Category = MovieCategory.MovieTrivia;

            }
            if (currentQuiz?.questions != null)
            {
                Questions = new ObservableCollection<Question>(currentQuiz.questions);
                OnPropertyChanged(nameof(Questions));
            }
        }

        public async Task<bool> Save()
        {
            if (currentQuiz == null)
                return false;

            if (string.IsNullOrWhiteSpace(QuizTitle))
            {
                MessageBox.Show("Please enter a quiz title.", "Validation",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Statement))
            {
                MessageBox.Show("Please enter a question.", "Validation",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            int nonEmptyAnswers = Answers.Count(a => !string.IsNullOrWhiteSpace(a));
            if (nonEmptyAnswers < 4)
            {
                MessageBox.Show("Please fill in all four answer options.", "Validation",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            currentQuiz.Title = (QuizTitle ?? "").Trim();
            var cleanedStatement = (Statement ?? "").Trim();
            var cleanedAnswers = (Answers ?? new string[4])
                .Select(a => a?.Trim() ?? "")
                .ToArray();

            string finalImageUrl = UseImage ? ImageUrl?.Trim() : null;

            if (currentQuestion == null)
            {
                currentQuiz.AddQuestion(cleanedStatement, CorrectAnswer, Category, cleanedAnswers);
                var justAdded = currentQuiz.questions.Last();
                justAdded.ImageUrl = finalImageUrl;
                Questions.Add(justAdded);
            }
            else
            {
                currentQuestion.Statement = cleanedStatement;
                currentQuestion.Answers = cleanedAnswers;
                currentQuestion.CorrectAnswer = CorrectAnswer;
                currentQuestion.ImageUrl = finalImageUrl;
                currentQuestion.Category = Category;
            }
            await JsonStorage.SaveQuizAsync(currentQuiz);

            var mgr = QuizManager.Instance;
            if (!mgr.quizzes.Any(q =>
                    string.Equals(q.FileName, currentQuiz.FileName, StringComparison.OrdinalIgnoreCase)))
            {
                mgr.quizzes.Add(currentQuiz);
            }

            currentQuestion = null;
            return true;
        }

        public async Task<bool> DeleteSelectedQuestion()
        {
            if (currentQuiz == null || SelectedQuestionIndex < 0)
                return false;

            currentQuiz.RemoveQuestion(SelectedQuestionIndex);
            Questions.RemoveAt(SelectedQuestionIndex);
            Clear();

            await JsonStorage.SaveQuizAsync(currentQuiz);
            OnPropertyChanged(nameof(Questions));

            return HandleQuizDeletionIfEmpty();
        }

        private bool HandleQuizDeletionIfEmpty()
        {
            if (currentQuiz.QuestionCount > 0)
                return false;

            MessageBox.Show(
                   "There are no questions left in this quiz.\nThe quiz will now be deleted.",
                   "Quiz deleted",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information);

            if (string.IsNullOrWhiteSpace(currentQuiz.FileName))
                currentQuiz.FileName = $"{currentQuiz.Title.Replace(" ", "_")}.json";

            JsonStorage.DeleteQuizFile(currentQuiz.FileName);
            QuizManager.Instance.quizzes.Remove(currentQuiz);

            return true;
        }
        public void Clear()
        {
            Statement = string.Empty;
            Answers = new string[4];
            CorrectAnswer = 0;
            ImageUrl = string.Empty;
            UseImage = false;

            Category = MovieCategory.MovieTrivia;

            OnPropertyChanged(nameof(Statement));
            OnPropertyChanged(nameof(CorrectAnswer));
            OnPropertyChanged(nameof(ImageUrl));
            OnPropertyChanged(nameof(UseImage));
            OnPropertyChanged(nameof(Category));
            OnPropertyChanged("Answers[0]");
            OnPropertyChanged("Answers[1]");
            OnPropertyChanged("Answers[2]");
            OnPropertyChanged("Answers[3]");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}



