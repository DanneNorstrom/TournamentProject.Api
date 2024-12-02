using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using TournamentProject.Core.Entities;
using TournamentProject.Core.Repositories;
using TournamentProject.Data.Data;
using TournamentProject.Data.Repositories;

namespace TournamentProject.Api.Controllers
{
    [Route("api/Games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly TournamentProjectApiContext _context;
        private UoW uow;

        public GamesController(TournamentProjectApiContext context)
        {
            _context = context;
            uow = new UoW(_context);
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGame()
        {
            var g = await uow.GameRepository.GetAllAsync();

            if (g == null)
                return NotFound();

            //return await _context.Game.ToListAsync();

            return Ok(g);
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var g = await uow.GameRepository.GetAsync(id);
            //var game = await _context.Game.FindAsync(id);

            if (g == null)
            {
                return NotFound();
            }

            return Ok(g);
        }

        /*[HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames(int tournamentId)
        {
            var tournamentExist = await _context.Tournament.AnyAsync(t => t.Id == tournamentId);

            if (!tournamentExist) return NotFound();

            var games = await _context.Game.Where(g => g.TournamentId.Equals(tournamentId)).ToListAsync();
            //var employeesDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            //return Ok(employeesDtos);
            return games;
        }*/

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            //_context.Entry(game).State = EntityState.Modified;
            uow.GameRepository.Update(game);

            try
            {
                //await _context.SaveChangesAsync();
                await uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {

            //_context.Game.Add(game);
            //await _context.SaveChangesAsync();

            uow.GameRepository.Add(game);
            await uow.CompleteAsync();

            return Created();

            //return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            //var game = await _context.Game.FindAsync(id);
            var game = await uow.GameRepository.GetAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            //_context.Game.Remove(game);
            //await _context.SaveChangesAsync();

            uow.GameRepository.Remove(game);
            await uow.CompleteAsync();

            return NoContent();
        }

        private bool GameExists(int id)
        {
            //return _context.Game.Any(e => e.Id == id);
            return uow.GameRepository.Any(id);
        }
    }
}
