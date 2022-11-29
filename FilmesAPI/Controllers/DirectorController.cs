using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace FilmesAPI.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    public class DirectorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DirectorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll(int? page)
        {
            const int itensForPage = 3;
            int pageNumber = page ?? 1;

            var directors = _context.Directors.AsNoTracking().Include(d => d.Movies).Take(10).ToList().OrderBy(x => x.Name);

            return Ok(await directors.ToPagedListAsync(pageNumber, itensForPage));
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
