using MediaLibrary.Data;
using MediaLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace MediaLibrary.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comments { get; set; }

        
        //Initialises inMem DB with default values instead of doing so in frontend.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Comment>() //defines one to many relationship between movie and comments.
                .HasOne(c => c.Movie)
                .WithMany(m => m.Comments)
                .HasForeignKey(c => c.MovieId);

            // Call method to initialise data
            MoviesAndComments.Initialise(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
