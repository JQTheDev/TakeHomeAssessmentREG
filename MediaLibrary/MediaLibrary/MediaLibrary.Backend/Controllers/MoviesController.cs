using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediaLibrary.EF;
using MediaLibrary.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            try
            {
                var movie = await _context.Movies.FindAsync(id);
                if (movie == null)
                {
                    return NotFound("A movie with this id does not exist");
                }
                else
                {
                    return Ok(movie);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                return StatusCode(500, new
                {
                    success = false,
                    message = $"An error occurred while trying to access the database: {ex.Message}"
                });
            }
        }
    }
}
