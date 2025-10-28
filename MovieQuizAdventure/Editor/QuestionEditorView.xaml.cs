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

            if (quiz == null)
            {
                quiz = new Quiz("");
                currentQuiz = quiz;
                IsEditMode = false;
                ViewModel = new EditAndCreatQuizViewModel(null, quiz);
            }
            else
            {
                currentQuiz = quiz;
                IsEditMode = question != null;
                ViewModel = new EditAndCreatQuizViewModel(question, quiz);
            }

            DataContext = ViewModel;
            NewQuizMode.Visibility = IsEditMode ? Visibility.Collapsed : Visibility.Visible;
            SaveQuizButton.Visibility = IsEditMode ? Visibility.Visible : Visibility.Collapsed;
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
        private async void SaveQuizClick(object sender, RoutedEventArgs e)
        {
            var ok = await ViewModel.Save();
            if (!ok) return;

            if (!IsEditMode)
            {
                mainWindow.Navigate(new MainMenuView(mainWindow));
            }
            else
            {
                mainWindow.Navigate(new SelectQuestionView(mainWindow, currentQuiz));
            }
        }
        private async void NextQuestionClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var ok = await ViewModel.Save();
                if (!ok) return;

                ViewModel.Clear();
                DataContext = null;
                DataContext = ViewModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Something went wrong when saving the question.\n\nDetails:\n{ex.Message}",
                    "Save Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

    }

}
