using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Monostori_Robert_backend.Models;

namespace Monostori_Robert_backend.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly CinemadbContext _context;

        public ActorsController(CinemadbContext context) {
            _context = context;
        }

        [HttpGet("Feladat 9")]

        public async Task<ActionResult<Actor>> Get(string name)
        {
            var actor = await _context.Actors.Include(act => act.Movies).FirstOrDefaultAsync(act => act.ActorName == name);

            if (actor != null)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpGet("12. feladat")]

        public async Task<ActionResult<string>> NumOfActors()
        {
            var num = await _context.Actors.CountAsync();
            if (num > 0)
            {
                return Ok($"színészek száma {num}");
            }
            return BadRequest("Nem lehet csatlakzoni az adatbázishoz.");
        }

        [HttpDelete("feladt 16")]
        public async Task<ActionResult<string>> DeleteActors(int id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(act => act.ActorId == id);

            if (actor != null)
            {
                _context.Actors.Remove(actor);
                await _context.SaveChangesAsync();
                return Ok("Sikeres törlés.");
            }
            return BadRequest("Nincs ilyen felhasználó.");
        }
        [HttpPut]


        public async Task<ActionResult<Actor>> Put(int id, UpdateActorDto updateActorDto)
        {
            var existingActor = await _context.Actors.FirstOrDefaultAsync(act => act.ActorId == id);

            if (existingActor != null)
            {
                existingActor.ActorName = updateActorDto.ActorName;
                _context.Actors.Update(existingActor);
                await _context.SaveChangesAsync();
                return Ok(existingActor);
            }
            return NotFound(new { message = "Nincs ilyen" });
        }

        [HttpPost]

        public async Task<ActionResult<Actor>> Post(CreateActorDto createActorDto)
        {
            var Actor = new Actor
            {
                ActorId = createActorDto.ActorId,
                ActorName = createActorDto.ActorName,
            };
            if (Actor != null)
            {
                await _context.Actors.AddAsync(Actor);
                await _context.SaveChangesAsync();
                return StatusCode(201, Actor);
            }
            return BadRequest();
        }
    }
}
