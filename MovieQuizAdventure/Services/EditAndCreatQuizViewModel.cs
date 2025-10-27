using MovieQuizAdventure.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MovieQuizAdventure.Services
{
    public class EditAndCreatQuizViewModel
    {

        private Quiz currentQuiz;
        private Question currentQuestion;

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
        public string Statement { get; set; }
        public string[] Answers { get; set; } = new string[4];
        public int CorrectAnswer { get; set; }

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
        }

        public void Save()
        {
            if (currentQuiz == null)
                return;

            currentQuiz.Title = QuizTitle;

            if (currentQuestion == null)
            {
                currentQuiz.AddQuestion(Statement, CorrectAnswer, Answers);
            }
            else
            {
                currentQuestion.Statement = Statement;
                currentQuestion.Answers = Answers;
                currentQuestion.CorrectAnswer = CorrectAnswer;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


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
    }
}



