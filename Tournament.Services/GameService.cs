using AutoMapper;
using Microsoft.VisualBasic;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TournamentProject.Core.Dto;
using TournamentProject.Core.Entities;
using TournamentProject.Core.Repositories;

namespace TournamentProject.Services
{
    public class GameService : IGameService
    {
        private IUoW _uoW;
        private readonly IMapper _mapper;

        public GameService(IUoW uoW, IMapper mapper)
        {
            _uoW = uoW;
            _mapper = mapper;
        }

        public async Task<GameDto> GetAsync(int id)
        {
            return _mapper.Map<GameDto>(await _uoW.GameRepository.GetAsync(id));
        }
        public async Task<Status> Remove(int id)
        {
            var game = await _uoW.GameRepository.GetAsync(id);

            if(game == null) 
                return new Status() { Message = "notfound"};
            
            _uoW.GameRepository.Remove(game);

            try
            {
                await _uoW.CompleteAsync();
            }

            catch (Exception ex)
            {
                return new Status() { Message = "saveerror" };
            }

            return new Status() { Message = "ok" };
        }

        public async Task<Status> Add(AddGameDto agDto)
        {
            if (await IsAddGameOk(agDto.TournamentId))
            {
                var game = _mapper.Map<Game>(agDto);

                _uoW.GameRepository.Add(game);

                try
                {
                    await _uoW.CompleteAsync();
                }

                catch (Exception ex)
                {
                    return new Status() { Message = "saveerror" };
                }

                return new Status() { Message = "ok" };
            }

            else return new Status() { Message = "tournamentfull" };
        }

        public async Task<bool> IsAddGameOk(int id)
        {
            var t = await _uoW.TournamentRepository.GetAsync(id, true);

            if(t.Games.Count >= 10) return false;
            else return true;
        }

        public async Task<int> CountAsync()
        {
            return await _uoW.GameRepository.CountAsync();
        }
        public async Task<IEnumerable<PagingGameDto>> PagingAsync(int page, int pageSize)
        {
            return _mapper.Map<IEnumerable<PagingGameDto>>(await _uoW.GameRepository.PagingAsync(page, pageSize));
        }
    }
}
