namespace MediaLibrary.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Rating { get; set; }
        public int Duration { get; set; }
        public int Year { get; set; }
        public int Popularity { get; set; }
        public List<Comment>? Comments { get; set; } = new List<Comment>(); //Comments can be null
    }

    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }

        //Allows EF to understand relationship between comment and movie. One movie can have many comments. MovieId is used as Foreign key.
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
