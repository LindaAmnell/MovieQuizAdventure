using MovieQuizAdventure.Models;
using System.IO;
using System.Text.Json;
namespace MovieQuizAdventure.Services
{
    public static class JsonStorage
    {

        private static string folderPath =>
             Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MovieQuizAdventure");


        static JsonStorage()
        {
            Directory.CreateDirectory(folderPath);
        }

        public static async Task SaveQuizAsync(Quiz quiz)
        {
            if (quiz?.questions == null || quiz.questions.Count == 0)
                return;
            string safeTitle = quiz.Title.Replace(" ", "_");
            string fileName = quiz.FileName ?? $"{safeTitle}.json";
            quiz.FileName = fileName;

            string fullPath = Path.Combine(folderPath, fileName);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(quiz, options);
            await File.WriteAllTextAsync(fullPath, json);
        }

        public static async Task<Quiz?> LoadQuizAsync(string title)
        {
            string fullPath = Path.Combine(folderPath, $"{title}.json");

            if (!File.Exists(fullPath))
                return null;

            string json = await File.ReadAllTextAsync(fullPath);
            Quiz loadedQuiz = JsonSerializer.Deserialize<Quiz>(json);
            loadedQuiz.FileName = $"{title}.json";

            return loadedQuiz;
        }

        public static List<string> GetSavedQuizFiles()
        {
            return Directory.GetFiles(folderPath, "*.json")
                            .Select(Path.GetFileNameWithoutExtension)
                            .ToList();
        }

        public static void DeleteQuizFile(string fileName)
        {
            string fullPath = Path.Combine(folderPath, fileName);

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }


        public static void EnsureDefaultQuizzesFromProject()
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
    }
}
