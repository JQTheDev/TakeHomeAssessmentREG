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

    }
}
