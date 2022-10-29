using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MovieController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllMovies")]
        public IActionResult GetAll()
        {
            var movies = _context.Movies.Include(d => d.Director).Take(10).ToList();

            return Ok(movies);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var movie = _context.Movies.Find(id);

            if (movie is null) return NotFound("Can't find the movie");

            return Ok(movie);
        }

        [HttpGet("{title}")]
        public IActionResult GetByTitle(string title)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Title == title);

            if (movie is null) return NotFound("Can't find the movie");

            return Ok(movie);
        }

        [HttpPost]
        public IActionResult Post(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();

            return Ok(movie);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, Movie movie)
        {
            if (id != movie.Id) return BadRequest();

            _context.Entry(movie).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(movie);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var movie = _context.Movies.Find(id);

            _context.Movies.Remove(movie);

            return Ok(movie);
        }
    }
}
