using AutoMapper;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentProject.Core.Dto;
using TournamentProject.Core.Entities;
using TournamentProject.Core.Repositories;
//using static Tournament.Services.TournamentService;

namespace TournamentProject.Services
{
    public class TournamentService : ITournamentService
    {
        private IUoW _uoW;
        private IMapper _mapper;

        public TournamentService(IUoW uoW, IMapper mapper)
        {
            _uoW = uoW;
            _mapper = mapper;
        }

        /*Task<IEnumerable<TournamentDto>> GetAllAsync(bool includeGames)
        {
            var tDto = _mapper.Map<IEnumerable<TournamentDto>>(await _uoW.TournamentRepository.GetAllAsync(includeGames));

            if (tDto == null)
                return NotFound();

            return Ok(tDto);
        }*/

        public async Task<IEnumerable<TournamentDto>> GetAllAsync(bool includeGames)
        {
            return _mapper.Map<IEnumerable<TournamentDto>>(await _uoW.TournamentRepository.GetAllAsync(includeGames));

           //var tDto = await _uoW.TournamentRepository.GetAllAsync(includeGames);

           //var tDto2 = _mapp.Map<IEnumerable<TournamentDto>>(tDto);


            //if (tDto == null)
                //return NotFound();

            //tDto = null;
            
            //return tDto2;
            //return null;
        }
    }
}
