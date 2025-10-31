using MovieQuizAdventure.Services;
using System.Windows;

namespace MovieQuizAdventure
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            JsonStorage.EnsureDefaultQuizzesFromProject();

            await QuizManager.Instance.LoadAllSavedQuizzes();

            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }

}
