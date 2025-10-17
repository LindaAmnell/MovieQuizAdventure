using System.Windows;
using System.Windows.Controls;

namespace MovieQuizAdventure
{
    public partial class MainMenuView : UserControl
    {
        private MainWindow mainWindow;


        public MainMenuView(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
        }

        private void PlayButtonClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate(new SelectQuiz(mainWindow));
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate(new EditMenuView(mainWindow));
        }
    }
}
