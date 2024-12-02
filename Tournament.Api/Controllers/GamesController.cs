﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using TournamentProject.Core.Dto;
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
        private IMapper _mapper;

        public GamesController(TournamentProjectApiContext context, IMapper mapper)
        {
            _context = context;
            uow = new UoW(_context);
            _mapper = mapper;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGame()
        {
            //var g = await uow.GameRepository.GetAllAsync();

            var gDto = _mapper.Map<IEnumerable<GameDto>>(await uow.GameRepository.GetAllAsync());


            if (gDto == null)
                return NotFound();

            //return await _context.Game.ToListAsync();

            return Ok(gDto);
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(int id)
        {
            var tDto = _mapper.Map<GameDto>(await uow.GameRepository.GetAsync(id));


            //var g = await uow.GameRepository.GetAsync(id);
            //var game = await _context.Game.FindAsync(id);

            if (tDto == null)
            {
                return NotFound();
            }

            return Ok(tDto);
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, UpdateGameDto ugDto)
        {
            bool saveerror = false;

            if (id != ugDto.Id || ugDto.Title.Length > 40)
            {
                return BadRequest();
            }

            var game = _mapper.Map<Game>(ugDto);
            //_context.Entry(game).State = EntityState.Modified;
            uow.GameRepository.Update(game);

            try
            {
                //await _context.SaveChangesAsync();
                await uow.CompleteAsync();
            }
            catch
            {
                saveerror = true;
            }

            if (saveerror) { return StatusCode(500); }
            else { return Ok(); }
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostGame(AddGameDto agDto)
        {
            bool saveerror = false;

            //_context.Game.Add(game);
            //await _context.SaveChangesAsync();

            var game = _mapper.Map<Game>(agDto);

            uow.GameRepository.Add(game);

            try
            {
                await uow.CompleteAsync();
            }

            catch
            {
                saveerror = true;
            }

            if (saveerror) { return StatusCode(500); }
            else { return Ok(); }

            //return Created();
            //return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            bool saveerror = false;

            //var game = await _context.Game.FindAsync(id);
            var game = await uow.GameRepository.GetAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            //_context.Game.Remove(game);
            //await _context.SaveChangesAsync();

            uow.GameRepository.Remove(game);

            try
            {
                await uow.CompleteAsync();
            }

            catch
            {
                saveerror = true;
            }

            if (saveerror) { return StatusCode(500); }
            else { return Ok(); }
        }

        /*private bool GameExists(int id)
        {
            //return _context.Game.Any(e => e.Id == id);
            return uow.GameRepository.Any(id);
        }*/

        [HttpPatch("{id}")]
        public async Task<ActionResult>PatchGame(int id, JsonPatchDocument<UpdateGameDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("No patch document");
            }

            var gameToPatch = await uow.GameRepository.GetAsync(id);

            if (gameToPatch == null)
            { 
                return NotFound("Game not found");
            }

            var ugDto = _mapper.Map<UpdateGameDto> (gameToPatch);

            patchDocument.ApplyTo(ugDto, ModelState);
            TryValidateModel(ugDto);

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            //_mapper.Map(ugDto, gameToPatch);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
