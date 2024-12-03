using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TournamentProject.Core.Dto;
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
        private IMapper _mapper;


        public TournamentsController(TournamentProjectApiContext context, IMapper mapper)
        //public TournamentsController(IUoW uoW)

        {
            _context = context;
            uow = new UoW(_context);
            _mapper = mapper;
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournament()
        {
            //var t = await uow.TournamentRepository.GetAllAsync();
            
            var tDto = _mapper.Map<IEnumerable<TournamentDto>>(await uow.TournamentRepository.GetAllAsync());

            if (tDto == null)
                return NotFound();

            //return await _context.Tournament.ToListAsync();

            return Ok(tDto);
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDto>> GetTournament(int id)
        {
            var tDto = _mapper.Map<TournamentDto>(await uow.TournamentRepository.GetAsync(id));

            //var tournament = await uow.TournamentRepository.GetAsync(id);
            //var tournament = await _context.Tournament.FindAsync(id);

            if (tDto == null)
            {
                return NotFound();
            }

            return Ok(tDto);
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, UpdateTournamentDto utDto)
        {
            if (id != utDto.Id)
            {
                return BadRequest();
            }

            var tournament = _mapper.Map<Tournament>(utDto);
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
        public async Task<IActionResult>PostTournament(AddTournamentDto atDto)
        {
            //_context.Tournament.Add(tournament);
            //await _context.SaveChangesAsync();

            var tournament = _mapper.Map<Tournament>(atDto);


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
