

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
    [Route("api/Games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        //private readonly TournamentProjectContext _context;
        //private readonly IUoW _uoW;
        //private readonly IMapper _mapper;
        private readonly IServiceManager _sm;

        //public GamesController(TournamentProjectContext context, IUoW uoW, IMapper mapper, IServiceManager sm)
        public GamesController(IServiceManager sm)

        {
            //_context = context;
            //_uoW = uoW;
            //_mapper = mapper;
            _sm = sm;
        }

        // GET: api/Games?page=2&pageSize=50
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagingGameDto>>> GetGames(int page, int pageSize)
        {
            //var g = await uow.GameRepository.GetAllAsync();

            //tDto.totalItems = await _context.Game.CountAsync();

            //var gDto = _mapper.Map<IEnumerable<GameDto>>(await _uoW.GameRepository.GetAllAsync());

            if (pageSize == 0)
            {
                pageSize = 20;
            }

            else
            {
                if (pageSize > 100)
                {
                    pageSize = 100;
                }
            }

            //var ti = await _context.Game.CountAsync();
            var ti = await _sm.GameService.CountAsync();
            var tisave = ti;
            int tp = 0;

            do
            {
                tp++;

                if (ti - pageSize <= 0) break;
                else ti -= pageSize;

            } while (true);

            var gDto = await _sm.GameService.PagingAsync(page, pageSize);

            if (gDto == null)
                return Problem(statusCode: 404);

            /*var games = await _context.Game
               .OrderBy(g => g.Id)
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();*/

            //var gDto = _mapper.Map<IEnumerable<GameDto>>(games);

            gDto.ElementAt(0).currentPage = page;
            gDto.ElementAt(0).pageSize = pageSize;
            gDto.ElementAt(0).totalItems = tisave;
            gDto.ElementAt(0).totalPages = tp;
   
           
            //return await _context.Game.ToListAsync();

            return Ok(gDto);
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(int id)
        {
            //var gDto = _mapper.Map<GameDto>(await _uoW.GameRepository.GetAsync(id));

            var gDto = await _sm.GameService.GetAsync(id);

            //var g = await uow.GameRepository.GetAsync(id);
            //var game = await _context.Game.FindAsync(id);

            if (gDto == null)
            {
                return Problem(statusCode: 404);
            }

            return Ok(gDto);
        }

        // GET: api/Games/Search?Title=Poker
        /*[Route("Search")]
        [HttpGet]

        public async Task<ActionResult<IEnumerable<GameDto>>> GetGames(string title)
        {
            var gDto = _mapper.Map<IEnumerable<GameDto>>(await _uoW.GameRepository.GetAllAsync(title));

            return Ok(gDto);
        }*/


        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, UpdateGameDto ugDto)
        {
            bool saveerror = false;

            if (id != ugDto.Id || ugDto.Title.Length > 40)
            {
                return BadRequest();
            }

            var game = _mapper.Map<Game>(ugDto);
            //_context.Entry(game).State = EntityState.Modified;
            _uoW.GameRepository.Update(game);

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
        }*/

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostGame(AddGameDto agDto)
        {
            //bool saveerror = false;

            //_context.Game.Add(game);
            //await _context.SaveChangesAsync();

            var status = await _sm.GameService.Add(agDto);

            /*var game = _mapper.Map<Game>(agDto);

            _uoW.GameRepository.Add(game);

            try
            {
                await _uoW.CompleteAsync();
            }

            catch
            {
                saveerror = true;
            }

            if (saveerror) { return StatusCode(500); }
            else { return Ok(); }*/

            if (status.Message == "saveerror") return Problem(statusCode: 500);
            else if (status.Message == "notfound") return Problem(statusCode: 404);
            else if (status.Message == "tournamentfull") return Problem(statusCode: 406);
            return Ok();

            //return Created();
            //return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            //bool saveerror = false;

            //var game = await _context.Game.FindAsync(id);
            //var game = await _uoW.GameRepository.GetAsync(id);
            //var game = await _sm.GameService.GetAsync(id);

            var status = await _sm.GameService.Remove(id);

            //_context.Game.Remove(game);
            //await _context.SaveChangesAsync();

            //_uoW.GameRepository.Remove(game);

            //_sm.GameService.Remove(game);

            /*try
            {
                await _uoW.CompleteAsync();
            }

            catch
            {
                saveerror = true;
            }*/

            if (status.Message == "saveerror") return Problem(statusCode: 500); 
            else if (status.Message == "notfound") return Problem(statusCode: 404);
            return Ok(); 
        }

        /*private bool GameExists(int id)
        {
            //return _context.Game.Any(e => e.Id == id);
            return uow.GameRepository.Any(id);
        }*/

        /*[HttpPatch("{id}")]
        public async Task<ActionResult> PatchGame(int id, JsonPatchDocument<UpdateGameDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("No patch document");
            }

            var gameToPatch = await _uoW.GameRepository.GetAsync(id);

            if (gameToPatch == null)
            {
                return NotFound("Game not found");
            }

            var ugDto = _mapper.Map<UpdateGameDto>(gameToPatch);

            patchDocument.ApplyTo(ugDto, ModelState);
            TryValidateModel(ugDto);

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            //_mapper.Map(ugDto, gameToPatch);
            //await _context.SaveChangesAsync();
            await _uoW.CompleteAsync();

            return NoContent();
        }*/
    }
}
