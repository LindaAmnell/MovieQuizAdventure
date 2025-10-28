using MovieQuizAdventure.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MovieQuizAdventure.Services
{
    public class EditAndCreatQuizViewModel
    {

        private Quiz currentQuiz;
        private Question currentQuestion;
        public Quiz CurrentQuiz => currentQuiz;
        public string Statement { get; set; }
        public string[] Answers { get; set; } = new string[4];
        public int CorrectAnswer { get; set; }
        public ObservableCollection<Question> Questions { get; private set; } = new();

        private int _selectedQuestionIndex = -1;
        public int SelectedQuestionIndex
        {
            get => _selectedQuestionIndex;
            set
            {
                if (_selectedQuestionIndex != value)
                {
                    _selectedQuestionIndex = value;
                    OnPropertyChanged();
                }
            }
        }

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
            }
            else
            {
                Statement = string.Empty;
                Answers = new string[4];
                CorrectAnswer = 0;
            }
            if (currentQuiz?.questions != null)
            {
                Questions = new ObservableCollection<Question>(currentQuiz.questions);
                OnPropertyChanged(nameof(Questions));
            }
        }

        public async Task Save()
        {
            if (currentQuiz == null)
                return;

            currentQuiz.Title = QuizTitle;

            if (currentQuestion == null)
            {
                currentQuiz.AddQuestion(Statement, CorrectAnswer, Answers);
                Questions.Add(currentQuiz.questions.Last());
            }
            else
            {
                currentQuestion.Statement = Statement;
                currentQuestion.Answers = Answers;
                currentQuestion.CorrectAnswer = CorrectAnswer;
            }
            await JsonStorage.SaveQuizAsync(currentQuiz);
            currentQuestion = null;
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
            return true;
        }


        public void Clear()
        {
            Statement = string.Empty;
            Answers = new string[4];
            CorrectAnswer = 0;

            OnPropertyChanged(nameof(Statement));
            OnPropertyChanged(nameof(CorrectAnswer));
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



