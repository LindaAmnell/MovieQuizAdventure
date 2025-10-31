using MovieQuizAdventure.Models;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
namespace MovieQuizAdventure.Services
{
    public static class JsonStorage
    {

        private static string folderPath =>
             Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MovieQuizAdventure");

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() },
            PropertyNameCaseInsensitive = true
        };

        static JsonStorage()
        {
            try
            {

                Directory.CreateDirectory(folderPath);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not create storage folder:\n{folderPath}\n\n{ex.Message}",
                               "Storage initialization error",
                               MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public static async Task SaveQuizAsync(Quiz quiz)
        {
            try
            {
                if (quiz?.questions == null || quiz.questions.Count == 0)
                    return;
                string safeTitle = quiz.Title.Replace(" ", "_");
                string fileName = quiz.FileName ?? $"{safeTitle}.json";
                quiz.FileName = fileName;

                string fullPath = Path.Combine(folderPath, fileName);

                string json = JsonSerializer.Serialize(quiz, JsonOptions);
                await File.WriteAllTextAsync(fullPath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save quiz \"{quiz?.Title}\":\n{ex.Message}",
                               "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static async Task<Quiz?> LoadQuizAsync(string title)
        {
            try
            {
                string fullPath = Path.Combine(folderPath, $"{title}.json");

                if (!File.Exists(fullPath))
                    return null;

                string json = await File.ReadAllTextAsync(fullPath);
                var loadedQuiz = JsonSerializer.Deserialize<Quiz>(json, JsonOptions);
                if (loadedQuiz != null)
                {
                    loadedQuiz.FileName = $"{title}.json";
                    loadedQuiz.Randomizer = new Random();
                }

                return loadedQuiz;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load quiz \"{title}\":\n{ex.Message}",
                                 "Load error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

        }

        public static List<string> GetSavedQuizFiles()
        {
            try
            {
                return Directory.GetFiles(folderPath, "*.json")
                                .Select(Path.GetFileNameWithoutExtension)
                                .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to list saved quizzes:\n{ex.Message}",
                                "Storage error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<string>();
            }
        }

        public static void DeleteQuizFile(string fileName)
        {
            try
            {
                string fullPath = Path.Combine(folderPath, fileName);
                if (File.Exists(fullPath))
                    File.Delete(fullPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete quiz file \"{fileName}\":\n{ex.Message}",
                                "Delete error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public static void EnsureDefaultQuizzesFromProject()
        {
            try
            {
                string firstRunFlag = Path.Combine(folderPath, "defaults_copied.txt");
                if (File.Exists(firstRunFlag))
                    return;

                string quizzesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Quizzes");
                if (Directory.Exists(quizzesFolder))
                {
                    foreach (var file in Directory.GetFiles(quizzesFolder, "*.json"))
                    {
                        string fileName = Path.GetFileName(file);
                        string destination = Path.Combine(folderPath, fileName);
                        if (!File.Exists(destination))
                            File.Copy(file, destination);
                    }
                }
                File.WriteAllText(firstRunFlag, "done");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to copy default quizzes:\n{ex.Message}",
                                "Initialization warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

