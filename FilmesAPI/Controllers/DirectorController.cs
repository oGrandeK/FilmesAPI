using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DirectorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllDirectors")]
        public IActionResult GetAll()
        {
            var directors = _context.Directors.Include(d => d.Movies).Take(10).ToList();

            return Ok(directors);
        }

        [HttpPost]
        public ActionResult Post(Director director)
        {
            _context.Directors.Add(director);
            _context.SaveChanges();

            return Ok(director);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, Director director)
        {
            if (id != director.Id) return BadRequest();

            _context.Entry(director).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(director);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var director = _context.Directors.Find(id);

            _context.Remove(director);
            _context.SaveChanges();

            return Ok(director);
        }
    }
}
