using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediaLibrary.EF;
using MediaLibrary.Models;

namespace MediaLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            try
            {
                var allMovies = await _context.Movies.ToListAsync();
                return Ok(allMovies);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Api error: {ex.Message}");
                return StatusCode(500);
            }

        }

        [HttpGet("{id}/Comments")]
        public async Task<ActionResult<Movie>> GetMovieComments(int id)
        {
            try
            {
                var comments = await _context.Comments
                    .Where(c => c.MovieId == id)
                    .ToListAsync();

                if (!comments.Any())
                {
                    return NotFound("No comments found for this movie.");
                }
                comments = comments.OrderByDescending(x => x.Date).ToList(); //ordering increases time Complex but not by much.

                return Ok(comments);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Api error: {ex.Message}");
                return StatusCode(500);
            }
        } //GetMovies returns all info on the movie apart from comments. When "More Info" button is actioned in UI, comments for corresponding
          //movie are returned to complete all the information. Comments are not returned in GetMovies call because of the relationship
          //between comments and movies in DB
    }
}
