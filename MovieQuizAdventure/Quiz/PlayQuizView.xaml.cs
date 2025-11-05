using MovieQuizAdventure.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        public async void AnswerBtn_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            int selectedIndex = int.Parse(clickedButton.Tag.ToString());
            foreach (var btn in AnswersPanel.Children.OfType<Button>())
                btn.IsEnabled = false;

            bool isCorrect = ViewModel.CurrentQuestion.CorrectAnswer == selectedIndex;
            if (isCorrect)
                clickedButton.Background = new SolidColorBrush(Colors.Green);
            else
            {
                clickedButton.Background = new SolidColorBrush(Colors.Red);
                Button correctBtn = AnswersPanel.Children
                    .OfType<Button>()
                    .First(b => int.Parse(b.Tag.ToString()) == ViewModel.CurrentQuestion.CorrectAnswer);

                correctBtn.Background = new SolidColorBrush(Colors.Green);
            }

            await Task.Delay(800);

            foreach (var btn in AnswersPanel.Children.OfType<Button>())
            {
                btn.Background = new SolidColorBrush(Color.FromRgb(18, 49, 88));
                btn.IsEnabled = true;
            }

            ViewModel.GetNextQuestion(selectedIndex);

            if (ViewModel.CurrentQuestion == null)
            {
                mainWindow.Navigate(new ResultView(mainWindow, ViewModel));
            }
        }
    }
}
