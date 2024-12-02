using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentProject.Core.Entities;
using TournamentProject.Data.Data;
using TournamentProject.Data.Repositories;

namespace TournamentProject.Api.Controllers
{
    [Route("api/Tournaments")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly TournamentProjectApiContext _context;
        private UoW uow;


        public TournamentsController(TournamentProjectApiContext context)
        {
            _context = context;
            uow = new UoW(_context);
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tournament>>> GetTournament()
        {
            var t = await uow.TournamentRepository.GetAllAsync();
            
            if (t == null) 
                return NotFound();

            //return await _context.Tournament.ToListAsync();
 

            return Ok(t);
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tournament>> GetTournament(int id)
        {
            var tournament = await uow.TournamentRepository.GetAsync(id);
            //var tournament = await _context.Tournament.FindAsync(id);

            if (tournament == null)
            {
                return NotFound();
            }

            return Ok(tournament);
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, Tournament tournament)
        {
            if (id != tournament.Id)
            {
                return BadRequest();
            }

            //_context.Entry(tournament).State = EntityState.Modified;
            uow.TournamentRepository.Update(tournament);

            try
            {
                //await _context.SaveChangesAsync();
                await uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TournamentExists(id))
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

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tournament>> PostTournament(Tournament tournament)
        {
            //_context.Tournament.Add(tournament);
            //await _context.SaveChangesAsync();

            uow.TournamentRepository.Add(tournament);
            await uow.CompleteAsync();

            return Created();
            //return CreatedAtAction("GetTournament", new { id = tournament.Id }, tournament);
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            //var tournament = await _context.Tournament.FindAsync(id);
            var tournament = await uow.TournamentRepository.GetAsync(id);

            if (tournament == null)
            {
                return NotFound();
            }

            //_context.Tournament.Remove(tournament);
            //await _context.SaveChangesAsync();

            uow.TournamentRepository.Remove(tournament);
            await uow.CompleteAsync();
            
            return NoContent();
        }

        private bool TournamentExists(int id)
        {
            //return _context.Tournament.Any(e => e.Id == id);
            return uow.TournamentRepository.Any(id);
        }
    }
}
