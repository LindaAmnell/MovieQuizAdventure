using MovieQuizAdventure.Models;
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
        public PlayQuizGame ViewModel { get; set; }

        public PlayQuizView(MainWindow main, PlayQuizGame quizGame)
        {
            InitializeComponent();
            mainWindow = main;
            ViewModel = quizGame;
            DataContext = ViewModel;
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate(new MainMenuView(mainWindow));
        }

        public void AnswerBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int selectedIndex = int.Parse(button.Tag.ToString());
            ViewModel.GetNextQuestion(selectedIndex);

            if (ViewModel.CurrentQuestion == null)
            {
                mainWindow.Navigate(new ResultView(mainWindow, ViewModel));
            }
        }
    }
}
