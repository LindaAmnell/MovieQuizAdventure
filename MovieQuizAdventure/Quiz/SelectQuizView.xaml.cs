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
        private bool IsEditMode;

        public SelectQuizView(MainWindow main, bool isEditMode = false)
        {
            InitializeComponent();
            mainWindow = main;
            this.IsEditMode = isEditMode;

            if (isEditMode)
            {
                EditIconsPanel.Visibility = Visibility.Visible;
            }
            else
            {
                EditIconsPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate(new MainMenuView(mainWindow));
        }
        private void GoToChoosenQuiz(object sender, RoutedEventArgs e)
        {
            if (!IsEditMode)
            {
                mainWindow.Navigate(new PlayQuizView(mainWindow));
            }
        }
        private void EditQuiz(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate(new SelectQuestionView(mainWindow));
        }


    }
}
