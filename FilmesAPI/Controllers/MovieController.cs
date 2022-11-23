using FilmesAPI.Data;
using FilmesAPI.Models;
using FilmesAPI.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

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

        // Get all Movies
        [HttpGet("GetAllMovies")]
        public async Task<IActionResult> GetAllMovies(int? page)
        {
            const int itensForPage = 3;
            int pageNumber = (page ?? 1);

            return Ok(await _context.Movies.ToPagedListAsync(pageNumber, itensForPage));
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
        public async Task<IActionResult> GetByTitle(string title, int? page)
        {
            const int itensForPage = 3;
            int pageNumber = (page ?? 1);

            var movie = _context.Movies.Where(m => m.Title == title);

            if (movie is null) return NotFound("Can't find the movie");

            return Ok(await movie.ToPagedListAsync(pageNumber, itensForPage));
        }

        // Get movie by Genre
        [HttpGet("Genre")]
        public async Task<IActionResult> GetByGender(EnumGenre genre, int? page)
        {
            const int itensForPage = 3;
            int pageNumber = (page ?? 1);

            var movies = _context.Movies.Where(m => m.Genre == genre).ToList().OrderBy(x => x.Title);

            if (movies is null) return BadRequest();

            return Ok(await movies.ToPagedListAsync(pageNumber, itensForPage));
        }

        // Get movie by Director
        [HttpGet("getBy/{directorname}")]
        public async Task<IActionResult> GetByDirector(string directorname, int? page) 
        {
            const int itensForPage = 3;
            int pageNumber = (page ?? 1);
            var movies = _context.Movies.Where(m => m.Director.Name == directorname).ToList().OrderBy(x => x.Director);

            return Ok(await movies.ToPagedListAsync(pageNumber, itensForPage));
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
