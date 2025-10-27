using MovieQuizAdventure.Models;
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
            QuestionList.ItemsSource = currentQuiz.questions;

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
    }
}
