using MovieQuizAdventure.Models;
using System.Windows;
using System.Windows.Controls;

namespace MovieQuizAdventure
{
    /// <summary>
    /// Interaction logic for ResultView.xaml
    /// </summary>
    public partial class ResultView : UserControl
    {
        private MainWindow mainWindow;

        public ResultView(MainWindow main, PlayQuizGame ViewModel)
        {
            InitializeComponent();
            mainWindow = main;
            DataContext = ViewModel;
        }

        private void BackToMenuButtonClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate(new MainMenuView(mainWindow));
        }
    }
}
