using System.Windows.Controls;

namespace MovieQuizAdventure
{
    /// <summary>
    /// Interaction logic for CreateOrEditQuizView.xaml
    /// </summary>
    public partial class CreateOrEditQuizView : UserControl
    {
        private MainWindow mainWindow;

        public CreateOrEditQuizView(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
        }
    }
}
