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
        public PlayQuizGame ViewModel { get; set; }

        public ResultView(MainWindow main, PlayQuizGame game)
        {
            InitializeComponent();
            mainWindow = main;
            ViewModel = game;
            DataContext = ViewModel;
        }

        private void BackToMenuButtonClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate(new MainMenuView(mainWindow));
        }
    }
}
