using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Monostori_Robert_backend.Models;

namespace Monostori_Robert_backend.Controllers
{
    [Route("api/movies")]
    [ApiController]

    public class MovieController : ControllerBase
    {
    private readonly CinemadbContext _context;

        public MovieController(CinemadbContext context) {
         _context = context;
        }

        [HttpGet("Feladat 10")]
        public async Task<ActionResult<Movie>> Get()
        {
            var movies= await _context.Movies.ToListAsync();

            if (movies != null)
            {
                return Ok();
            }
            Exception e = new();
            return BadRequest(new {message = e.Message });
        }

        [HttpPost("13. feladat")]

        public async Task<ActionResult> AddNewMovie(string id, Movie movie)
        {
            var builder = WebApplication.CreateBuilder();
            string uid = builder.Configuration.GetValue<string>("Code");

            if (uid != null && uid == id)
            {
                var mov = new Movie
                {
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    ActorId = movie.ActorId,
                    FilmTypeId = movie.FilmTypeId,

                };

                if (mov != null)
                {
                    await _context.Movies.AddAsync(mov);
                    await _context.SaveChangesAsync();
                    return StatusCode(201, "Film hozzáadása sikeresen megtörtént.");
                }
                return StatusCode(201, "Film hozzáadása sikeresen megtörtént.");
            }
            return StatusCode(401, "Nincs jogosultsága.");
        }
    }
}
