using MediaLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace MediaLibrary.Data
{
    public static class MoviesAndComments
    {
        public static void Initialise(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "Inception", Rating = 8, Duration = 148, Year = 2010, Popularity = 95 },
                new Movie { Id = 2, Title = "The Dark Knight", Rating = 9, Duration = 152, Year = 2008, Popularity = 98 },
                new Movie { Id = 3, Title = "Interstellar", Rating = 10, Duration = 169, Year = 2014, Popularity = 97 },
                new Movie { Id = 4, Title = "The Matrix", Rating = 1, Duration = 136, Year = 1999, Popularity = 93 },
                new Movie { Id = 5, Title = "Gladiator", Rating = 4, Duration = 155, Year = 2000, Popularity = 90 },
                new Movie { Id = 6, Title = "The Shawshank Redemption", Rating = 5, Duration = 142, Year = 1994, Popularity = 100 },
                new Movie { Id = 7, Title = "The Godfather", Rating = 2, Duration = 175, Year = 1972, Popularity = 98 },
                new Movie { Id = 8, Title = "Pulp Fiction", Rating = 3, Duration = 154, Year = 1994, Popularity = 95 },
                new Movie { Id = 9, Title = "Fight Club", Rating = 7, Duration = 139, Year = 1999, Popularity = 92 },
                new Movie { Id = 10, Title = "Forrest Gump", Rating = 6, Duration = 142, Year = 1994, Popularity = 96 }
            );

            modelBuilder.Entity<Comment>().HasData(
                new Comment { Id = 1, Text = "Amazing movie!", Author = "User1", Date = DateTime.UtcNow, MovieId = 1 },
                new Comment { Id = 2, Text = "Great visual effects.", Author = "User2", Date = DateTime.UtcNow, MovieId = 1 },
                new Comment { Id = 3, Text = "A masterpiece.", Author = "User3", Date = DateTime.UtcNow, MovieId = 2 },
                new Comment { Id = 4, Text = "Best Joker performance!", Author = "User4", Date = DateTime.UtcNow, MovieId = 2 },
                new Comment { Id = 5, Text = "Mind-blowing!", Author = "User5", Date = DateTime.UtcNow, MovieId = 3 },
                new Comment { Id = 6, Text = "The Matrix changed my life!", Author = "User6", Date = DateTime.UtcNow, MovieId = 4 },
                new Comment { Id = 7, Text = "Red pill or blue pill?", Author = "User7", Date = DateTime.UtcNow, MovieId = 4 },
                new Comment { Id = 8, Text = "Groundbreaking for its time.", Author = "User8", Date = DateTime.UtcNow, MovieId = 4 },
                new Comment { Id = 9, Text = "Russell Crowe was incredible.", Author = "User9", Date = DateTime.UtcNow, MovieId = 5 },
                new Comment { Id = 10, Text = "Best movie of all time!", Author = "User10", Date = DateTime.UtcNow, MovieId = 6 },
                new Comment { Id = 11, Text = "Tim Robbins was phenomenal.", Author = "User11", Date = DateTime.UtcNow, MovieId = 6 },
                new Comment { Id = 12, Text = "Hope is a good thing.", Author = "User12", Date = DateTime.UtcNow, MovieId = 6 },
                new Comment { Id = 13, Text = "Iconic film.", Author = "User13", Date = DateTime.UtcNow, MovieId = 7 },
                new Comment { Id = 14, Text = "Marlon Brando was perfect.", Author = "User14", Date = DateTime.UtcNow, MovieId = 7 },
                new Comment { Id = 15, Text = "So quotable.", Author = "User15", Date = DateTime.UtcNow, MovieId = 8 },
                new Comment { Id = 16, Text = "A Tarantino masterpiece.", Author = "User16", Date = DateTime.UtcNow, MovieId = 8 },
                new Comment { Id = 17, Text = "I love the nonlinear storytelling.", Author = "User17", Date = DateTime.UtcNow, MovieId = 8 },
                new Comment { Id = 18, Text = "The twist at the end!", Author = "User18", Date = DateTime.UtcNow, MovieId = 9 },
                new Comment { Id = 19, Text = "You can't talk about Fight Club.", Author = "User19", Date = DateTime.UtcNow, MovieId = 9 },
                new Comment { Id = 20, Text = "Forrest Gump is such an inspiring story.", Author = "User20", Date = DateTime.UtcNow, MovieId = 10 },
                new Comment { Id = 21, Text = "Tom Hanks is brilliant.", Author = "User21", Date = DateTime.UtcNow, MovieId = 10 }
            );
        }
    }
}
