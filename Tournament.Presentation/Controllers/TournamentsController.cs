﻿

using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using TournamentProject.Core.Dto;
using TournamentProject.Core.Entities;
using TournamentProject.Core.Repositories;
using TournamentProject.Data.Data;

namespace TournamentProject.Presentation.Controllers
{
    [Route("api/Tournaments")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private TournamentProjectContext _context;
        private readonly IUoW _uoW;
        private readonly IMapper _mapper;
        private IServiceManager _sm;

        public TournamentsController(TournamentProjectContext context, IUoW uoW, IMapper mapper, IServiceManager sm)
        //public TournamentsController(IServiceManager sm)
        {
            _context = context;
            _uoW = uoW;
            _mapper = mapper;
            _sm = sm;
        }

        // GET: api/Tournaments
        /*[HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournament()
        {
            //var t = await uow.TournamentRepository.GetAllAsync();

            var tDto = _mapper.Map<IEnumerable<TournamentDto>>(await _uoW.TournamentRepository.GetAllAsync());

            if (tDto == null)
            return NotFound();

            //return await _context.Tournament.ToListAsync();

            return Ok(tDto);
        }*/


        // GET: api/Tournaments/5?includeGames=true
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDto>> GetTournament(int id, bool includeGames = false)
        {
            var tDto = _mapper.Map<TournamentDto>(await _uoW.TournamentRepository.GetAsync(id, includeGames));

            //var tournament = await uow.TournamentRepository.GetAsync(id);
            //var tournament = await _context.Tournament.FindAsync(id);

            if (tDto == null)
            {
                return NotFound();
            }

            return Ok(tDto);
        }

        // GET: api/Tournaments?includeGames=
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournaments(bool includeGames)
        {
            //var t = await _uoW.TournamentRepository.GetAllAsync(includeGames);

            var tDto = await _sm.TournamentService.GetAllAsync(includeGames);

            //var tDto = _mapper.Map<IEnumerable<TournamentDto>>(await _uoW.TournamentRepository.GetAllAsync(includeGames));

            if (tDto == null)
                return NotFound();

            return Ok(tDto);
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, UpdateTournamentDto utDto)
        {
            bool saveerror = false;

            if (id != utDto.Id || utDto.Title.Length > 40)
            {
                return BadRequest();
            }

            var tournament = _mapper.Map<Tournament>(utDto);
            //_context.Entry(tournament).State = EntityState.Modified;
            _uoW.TournamentRepository.Update(tournament);

            try
            {
                //await _context.SaveChangesAsync();
                await _uoW.CompleteAsync();
            }
            catch
            {
                saveerror = true;
            }

            if (saveerror) { return StatusCode(500); }
            else { return Ok(); }
        }

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostTournament(AddTournamentDto atDto)
        {
            bool saveerror = false;

            if (atDto.Title.Length > 40)
            {
                return BadRequest();
            }

            //_context.Tournament.Add(tournament);
            //await _context.SaveChangesAsync();

            var tournament = _mapper.Map<Tournament>(atDto);

            _uoW.TournamentRepository.Add(tournament);

            try
            {
                await _uoW.CompleteAsync();
            }

            catch
            {
                saveerror = true;
            }

            if (saveerror) { return StatusCode(500); }
            else { return Ok(); }
            //return CreatedAtAction("GetTournament", new { id = tournament.Id }, tournament);
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            bool saveerror = false;
            //var tournament = await _context.Tournament.FindAsync(id);
            var tournament = await _uoW.TournamentRepository.GetAsync(id);

            if (tournament == null)
            {
                return NotFound();
            }

            //_context.Tournament.Remove(tournament);
            //await _context.SaveChangesAsync();

            _uoW.TournamentRepository.Remove(tournament);

            try
            {
                await _uoW.CompleteAsync();
            }

            catch
            {
                saveerror = true;
            }

            if (saveerror) { return StatusCode(500); }
            else { return Ok(); }
        }

        /*private bool TournamentExists(int id)
        {
            //return _context.Tournament.Any(e => e.Id == id);
            return uow.TournamentRepository.Any(id);
        }*/


        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchTournament(int id, JsonPatchDocument<UpdateTournamentDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("No patch document");
            }

            var tournamentToPatch = await _uoW.TournamentRepository.GetAsync(id);

            if (tournamentToPatch == null)
            {
                return NotFound("Tournament not found");
            }

            var utDto = _mapper.Map<UpdateTournamentDto>(tournamentToPatch);

            patchDocument.ApplyTo(utDto, ModelState);
            TryValidateModel(utDto);

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            //_mapper.Map(utDto, tournamentToPatch);
            //await _context.SaveChangesAsync();
            await _uoW.CompleteAsync();

            return NoContent();
        }
    }
}
