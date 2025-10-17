using System.Windows;
using System.Windows.Controls;

namespace MovieQuizAdventure
{
    /// <summary>
    /// Interaction logic for QuizSelectView.xaml
    /// </summary>
    public partial class SelectQuiz : UserControl
    {
        private MainWindow mainWindow;

        public SelectQuiz(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate(new MainMenuView(mainWindow));
        }
        private void GoToChoosenQuiz(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate(new PlayQuizView(mainWindow));
        }
    }
}
