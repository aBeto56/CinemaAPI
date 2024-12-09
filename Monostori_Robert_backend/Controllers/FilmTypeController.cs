using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Monostori_Robert_backend.Models;

namespace Monostori_Robert_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmTypeController : ControllerBase
    {
        private readonly CinemadbContext _context;

        public FilmTypeController(CinemadbContext context)
        {
            _context = context;
        }
        [HttpGet("Feladat 11")]
        public async Task<ActionResult<FilmType>> Get()
        {
            var filmtypes = await _context.FilmTypes.Include(flm => flm.Movies).ToListAsync();
            if (filmtypes != null)
            {
                return Ok(filmtypes);
            }
            Exception e = new();
            return BadRequest(new { message = e.Message});
        }

        [HttpPut]
        public async Task<ActionResult<FilmType>> Put(int id, UpdateFilmTypeDto updateFilmTypeDto)
        {
            var existingType = await _context.FilmTypes.FirstOrDefaultAsync(act => act.TypeId == id);

            if (existingType != null)
            {
                existingType.TypeName = updateFilmTypeDto.TypeName;
                _context.FilmTypes.Update(existingType);
                await _context.SaveChangesAsync();
                return Ok(existingType);
            }
            return NotFound(new { message = "Nincs ilyen" });
        }

        [HttpPost]

        public async Task<ActionResult<FilmType>> Post(CreateFilmTypeDto createFilmTypeDto)
        {
            var Type = new FilmType
            {
                TypeId = createFilmTypeDto.TypeId,
                TypeName = createFilmTypeDto.TypeName,
            };
            if (Type != null)
            {
                await _context.FilmTypes.AddAsync(Type);
                await _context.SaveChangesAsync();
                return StatusCode(201, Type);
            }
            return BadRequest();
        }

    }
}
