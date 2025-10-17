using System.Windows;
using System.Windows.Controls;

namespace MovieQuizAdventure
{
    /// <summary>
    /// Interaction logic for PlayQuizView.xaml
    /// </summary>
    public partial class PlayQuizView : UserControl
    {
        private MainWindow mainWindow;

        public PlayQuizView(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate(new MainMenuView(mainWindow));
        }

        private void ChossenAnswer(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate(new ResultView(mainWindow));
        }
    }
}
