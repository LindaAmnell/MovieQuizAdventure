using System.Windows;
using System.Windows.Controls;

namespace MovieQuizAdventure
{
    /// <summary>
    /// Interaction logic for CreateOrEditQuizView.xaml
    /// </summary>
    public partial class QuestionEditorView : UserControl
    {
        private MainWindow mainWindow;
        private bool IsEditMode;
        public QuestionEditorView(MainWindow main, bool isEditMode = false)
        {
            InitializeComponent();
            mainWindow = main;
            this.IsEditMode = isEditMode;
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            if (!IsEditMode)
            {
                mainWindow.Navigate(new MainMenuView(mainWindow));
            }
            else
            {
                mainWindow.Navigate(new SelectQuestionView(mainWindow));

            }
        }
        private void SaveQuizClick(object sender, RoutedEventArgs e)
        {
            if (!IsEditMode)
            {
                mainWindow.Navigate(new MainMenuView(mainWindow));
            }
            else
            {
                mainWindow.Navigate(new SelectQuestionView(mainWindow));

            }
        }

        private void CorrectAnswerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

}
