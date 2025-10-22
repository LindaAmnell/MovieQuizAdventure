using System.Windows;
using System.Windows.Controls;

namespace MovieQuizAdventure
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {



        public MainWindow()
        {

            var testQuiz = new MovieQuizAdventure.Models.Quiz("Movie Quiz");

            testQuiz.AddQuestion(
                "Who directed Inception?",
                0,
                "Christopher Nolan",
                "Steven Spielberg",
                "James Cameron");

            testQuiz.AddQuestion(
                "Which year was Titanic released?",
                1,
                "1995",
                "1997",
                "2001");

            testQuiz.AddQuestion(
                "Which movie won Best Picture at the 2020 Oscars?",
                2,
                "1917",
                "Joker",
                "Parasite");

            var game = new MovieQuizAdventure.Models.QuizGame(testQuiz);

            InitializeComponent();

            MainContent.Content = new MainMenuView(this);
        }

        public void Navigate(UserControl nextView)
        {
            MainContent.Content = nextView;
        }
    }

}