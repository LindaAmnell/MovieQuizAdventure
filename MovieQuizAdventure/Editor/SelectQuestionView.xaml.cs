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

        public SelectQuestionView(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate(new SelectQuizView(mainWindow));
        }

        private void EditQuestionClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate(new QuestionEditorView(mainWindow, isEditMode: true));
        }
    }
}
