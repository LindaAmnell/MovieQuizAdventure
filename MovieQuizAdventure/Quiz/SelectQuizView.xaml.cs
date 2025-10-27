using MovieQuizAdventure.Models;
using MovieQuizAdventure.Services;
using System.Windows;
using System.Windows.Controls;

namespace MovieQuizAdventure
{
    /// <summary>
    /// Interaction logic for QuizSelectView.xaml
    /// </summary>
    public partial class SelectQuizView : UserControl
    {
        private MainWindow mainWindow;
        public bool IsEditMode { get; set; }
        private QuizManager quizManager;

        public SelectQuizView(MainWindow main, bool isEditMode = false)
        {
            InitializeComponent();
            mainWindow = main;
            IsEditMode = isEditMode;
            DataContext = this;

            quizManager = new QuizManager();
            QuizList.ItemsSource = quizManager.GetAllQuizzes();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate(new MainMenuView(mainWindow));
        }
        private void GoToChoosenQuiz(object sender, RoutedEventArgs e)
        {
            if (!IsEditMode)
            {
                var button = sender as Button;
                var selectedQuiz = button?.Tag as Quiz;
                if (selectedQuiz == null) return;
                var game = new PlayQuizGame(selectedQuiz);
                mainWindow.Navigate(new PlayQuizView(mainWindow, game));
            }
        }

        private void EditQuiz(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var selectedQuiz = button?.Tag as Quiz;
            if (selectedQuiz == null) return;


            mainWindow.Navigate(new SelectQuestionView(mainWindow, selectedQuiz));
        }






    }
}
