using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MovieController(AppDbContext context)
        {
            _context = context;
        }

        // Get all Movies and directors
        [HttpGet("GetAllMovies")]
        public IActionResult GetAll()
        {
            var movies = _context.Movies.Include(d => d.Director).Take(10).ToList().OrderBy(x => x.Title);

            return Ok(movies);
        }

        // Get movie by Id
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var movie = _context.Movies.Find(id);

            if (movie is null) return NotFound("Can't find the movie");

            return Ok(movie);
        }

        // Get movie by Title
        [HttpGet("{title}")]
        public IActionResult GetByTitle(string title)
        {
            var movie = _context.Movies.Take(10).Where(m => m.Title == title);

            if (movie is null) return NotFound("Can't find the movie");

            return Ok(movie);
        }

        // Get movie by Genre
        [HttpGet("Genre")]
        public IActionResult GetByGender(EnumGenre genre)
        {
            var movies = _context.Movies.Where(m => m.Genre == genre).ToList().OrderBy(x => x.Title);

            if (movies is null) return BadRequest();

            return Ok(movies);
        }

        // Get movie by Director
        [HttpGet("getBy/{directorname}")]
        public IActionResult GetByDirector(string directorname) 
        {
            var movies = _context.Movies.Where(m => m.Director.Name == directorname).ToList().OrderBy(x => x.Director);

            return Ok(movies);
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
