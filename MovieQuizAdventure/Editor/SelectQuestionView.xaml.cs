using MovieQuizAdventure.Models;
using MovieQuizAdventure.Services;
using System.Windows;
using System.Windows.Controls;

namespace MovieQuizAdventure
{
    /// <summary>
    /// Interaction logic for SelectQuestionView.xaml
    /// </summary>
    public partial class SelectQuestionView : UserControl
    {
        private MainWindow mainWindow;
        private Quiz currentQuiz;

        public SelectQuestionView(MainWindow main, Quiz quiz)
        {
            InitializeComponent();
            mainWindow = main;
            currentQuiz = quiz;

            var ViewModel = new EditAndCreatQuizViewModel(null, currentQuiz);
            DataContext = ViewModel;

            QuestionList.ItemsSource = ViewModel.Questions;

        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate(new SelectQuizView(mainWindow, isEditMode: true));
        }

        private void EditQuestionClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var question = button?.Tag as Question;
            if (question == null) return;

            mainWindow.Navigate(new QuestionEditorView(mainWindow, question, currentQuiz));
        }

        private async void DeleteQuestionClick(object sender, RoutedEventArgs e)
        {
            var question = (sender as Button)?.Tag as Question;
            if (question == null) return;

            var vm = DataContext as EditAndCreatQuizViewModel;
            if (vm == null) return;

            vm.SelectedQuestionIndex = vm.Questions.IndexOf(question);
            if (vm.SelectedQuestionIndex < 0) return;

            bool quizDeleted = await vm.DeleteSelectedQuestion();

            if (quizDeleted)
            {
                mainWindow.Navigate(new SelectQuizView(mainWindow, isEditMode: true));
            }
        }
    }
}
