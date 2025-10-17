using System.Windows;
using System.Windows.Controls;

namespace MovieQuizAdventure
{
    /// <summary>
    /// Interaction logic for EditMenuView.xaml
    /// </summary>
    public partial class EditMenuView : UserControl
    {
        private MainWindow mainWindow;

        public EditMenuView(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate(new MainMenuView(mainWindow));
        }

    }
}
