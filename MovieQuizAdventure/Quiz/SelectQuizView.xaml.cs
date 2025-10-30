using MovieQuizAdventure.Models;
using MovieQuizAdventure.Services;
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
        public bool IsEditMode { get; set; }
        public bool IsPlayMode => !IsEditMode;

        private QuizManager quizManager;
        public SelectQuizView ViewModel { get; set; }
        public List<MovieCategory> MovieCategories { get; set; }

        public SelectQuizView(MainWindow main, bool isEditMode = false)
        {
            InitializeComponent();
            mainWindow = main;
            IsEditMode = isEditMode;

            quizManager = QuizManager.Instance;
            QuizList.ItemsSource = quizManager.quizzes;

            MovieCategories = Enum.GetValues(typeof(MovieCategory)).Cast<MovieCategory>().ToList();

            DataContext = this;
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate(new MainMenuView(mainWindow));
        }
        private void GoToChoosenQuiz(object sender, RoutedEventArgs e)
        {
            if (!IsEditMode)
            {
                var button = sender as Button;
                var selectedQuiz = button?.Tag as Quiz;
                if (selectedQuiz == null) return;
                var game = new PlayQuizGame(selectedQuiz);
                mainWindow.Navigate(new PlayQuizView(mainWindow, game));
            }
        }

        private void EditQuiz(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var selectedQuiz = button?.Tag as Quiz;
            if (selectedQuiz == null) return;


            mainWindow.Navigate(new SelectQuestionView(mainWindow, selectedQuiz));
        }


        private void DeleteQuestionClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var selectedQuiz = button?.Tag as Quiz;
            if (selectedQuiz == null) return;

            var result = MessageBox.Show(
                "There are no questions left in this quiz.\nThe quiz will now be deleted.",
                "Quiz deleted",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            JsonStorage.DeleteQuizFile(selectedQuiz.FileName);
            QuizManager.Instance.quizzes.Remove(selectedQuiz);

        }

        private void CategoryButtonClicked(object sender, RoutedEventArgs e)
        {
            var category = (MovieCategory)((Button)sender).CommandParameter;

            var allQuizzes = QuizManager.Instance.quizzes.ToList();
            Quiz categoryQuiz = PlayQuizGame.CreateQuizFromCategory(allQuizzes, category);

            if (categoryQuiz == null)
                return;

            var playGame = new PlayQuizGame(categoryQuiz);
            mainWindow.Navigate(new PlayQuizView(mainWindow, playGame));
        }

        //Visibility="{Binding DataContext.IsPlayMode,
        //                   RelativeSource={RelativeSource AncestorType = UserControl},
        //                   Converter={StaticResource BoolToVisibilityConverter}}"

    }
}
