using MovieQuizAdventure.Models;

namespace MovieQuizAdventure.Services
{
    public class QuizManager
    {
        private List<Quiz> quizzes;
        private Quiz currentQuiz;

        public QuizManager()
        {
            quizzes = new List<Quiz>();

            var movie = CreateQuiz("Movie Quiz");
            movie.AddQuestion("Who directed Inception?", 0, "Christopher Nolan", "Spielberg", "Cameron");
            movie.AddQuestion("Which year was Titanic released?", 1, "1995", "1997", "2001");
            movie.AddQuestion("Who played Jack in Titanic?", 0, "Leonardo DiCaprio", "Brad Pitt", "Tom Cruise");
            movie.AddQuestion("Which movie features the quote 'May the Force be with you'?", 1, "The Matrix", "Star Wars", "Star Trek");
            movie.AddQuestion("In The Dark Knight, who played the Joker?", 0, "Heath Ledger", "Joaquin Phoenix", "Jared Leto");
            movie.AddQuestion("Which film won Best Picture at the 1994 Oscars?", 2, "Pulp Fiction", "The Shawshank Redemption", "Forrest Gump");
            movie.AddQuestion("Who directed Avatar?", 2, "Steven Spielberg", "George Lucas", "James Cameron");
            movie.AddQuestion("What is the name of the kingdom in Frozen?", 1, "Narnia", "Arendelle", "Atlantis");
            movie.AddQuestion("In which year was the first Toy Story released?", 0, "1995", "1999", "2001");

            var sport = CreateQuiz("Sport Quiz");
            sport.AddQuestion("How many players in a football team?", 0, "11", "10", "12");
            sport.AddQuestion("Where were the 2016 Olympics held?", 1, "Tokyo", "Rio de Janeiro", "London");
            sport.AddQuestion("Which country won the FIFA World Cup in 2018?", 2, "Croatia", "Brazil", "France");
            sport.AddQuestion("In tennis, what is the term for 0 points?", 0, "Love", "Zero", "Nil");
            sport.AddQuestion("Who holds the record for the most Olympic gold medals?", 1, "Usain Bolt", "Michael Phelps", "Carl Lewis");
            sport.AddQuestion("In which sport is the term 'birdie' used?", 2, "Cricket", "Baseball", "Golf");
            sport.AddQuestion("What color flag is waved to signal the end of a Formula 1 race?", 0, "Checkered", "Red", "Green");
            sport.AddQuestion("Which country hosted the 2022 FIFA World Cup?", 2, "Russia", "USA", "Qatar");
            sport.AddQuestion("How many rings are on the Olympic flag?", 1, "4", "5", "6");

        }

        public Quiz CreateQuiz(string title)
        {
            var newQuiz = new Quiz(title);
            quizzes.Add(newQuiz);
            currentQuiz = newQuiz;
            return newQuiz;
        }

        public List<Quiz> GetAllQuizzes()
        {
            return quizzes;
        }

        public Quiz GetQuizByTitle(string title)
        {
            return quizzes.FirstOrDefault(q => q.Title == title);
        }

        public void SetCurrentQuiz(Quiz quiz)
        {
            currentQuiz = quiz;
        }

        public Quiz GetCurrentQuiz()
        {
            return currentQuiz;
        }
        public bool NewQuestion(string statement, string[] answers, int correctAnswer)
        {
            if (currentQuiz == null)
                return false;

            currentQuiz.AddQuestion(statement, correctAnswer, answers);
            return true;
        }
        public Question EditQuestion(int index, string newState, string[] newAnswers, int newCorrectAnswer)
        {
            if (currentQuiz == null) return null;
            if (index < 0 || index >= currentQuiz.questions.Count) return null;

            var updatedQuestion = new Question(newState, newAnswers, newCorrectAnswer);
            currentQuiz.questions[index] = updatedQuestion;
            return updatedQuestion;
        }
        public bool RenameQuiz(string newTitle)
        {
            if (currentQuiz == null || string.IsNullOrWhiteSpace(newTitle))
                return false;

            currentQuiz.Title = newTitle;
            return true;
        }
    }
}
