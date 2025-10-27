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
            mainWindow.Navigate(new SelectQuizView(mainWindow, isEditMode: false));
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate(new SelectQuizView(mainWindow, isEditMode: true));
        }
        private void CreatNewQuiz(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate(new QuestionEditorView(mainWindow));
        }


    }
}
