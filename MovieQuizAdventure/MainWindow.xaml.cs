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
            InitializeComponent();

            MainContent.Content = new MainMenuView(this);
        }

        public void Navigate(UserControl nextView)
        {
            MainContent.Content = nextView;
        }
    }

}