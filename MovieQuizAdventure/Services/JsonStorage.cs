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

    }
}
