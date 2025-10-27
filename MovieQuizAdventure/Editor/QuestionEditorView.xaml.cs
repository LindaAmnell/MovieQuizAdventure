using MovieQuizAdventure.Models;
using MovieQuizAdventure.Services;
using System.Windows;
using System.Windows.Controls;

namespace MovieQuizAdventure
{
    /// <summary>
    /// Interaction logic for CreateOrEditQuizView.xaml
    /// </summary>
    public partial class QuestionEditorView : UserControl
    {
        private MainWindow mainWindow;
        private Quiz currentQuiz;
        private bool IsEditMode;

        public EditAndCreatQuizViewModel ViewModel { get; set; }

        public QuestionEditorView(MainWindow main, Question question = null, Quiz quiz = null)
        {
            InitializeComponent();
            mainWindow = main;
            currentQuiz = quiz;

            if (question != null)
            {
                IsEditMode = true;
                ViewModel = new EditAndCreatQuizViewModel(question, quiz);
            }
            else
            {
                IsEditMode = false;
                ViewModel = new EditAndCreatQuizViewModel(null, quiz);
            }

            DataContext = ViewModel;
            NewQuizMode.Visibility = IsEditMode ? Visibility.Collapsed : Visibility.Visible;
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            if (!IsEditMode)
            {
                mainWindow.Navigate(new MainMenuView(mainWindow));
            }
            else
            {
                mainWindow.Navigate(new SelectQuestionView(mainWindow, currentQuiz));

            }
        }
        private void SaveQuizClick(object sender, RoutedEventArgs e)
        {
            if (!IsEditMode)
            {
                mainWindow.Navigate(new MainMenuView(mainWindow));
            }
            else
            {
                mainWindow.Navigate(new SelectQuestionView(mainWindow, currentQuiz));
                ViewModel.Save();
            }
        }
        private void NextQuestionClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Save();
            ViewModel.Clear();
            DataContext = null;
            DataContext = ViewModel;
        }
    }

}
