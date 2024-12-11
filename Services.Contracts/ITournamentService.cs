using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentProject.Core.Dto;
using TournamentProject.Core.Entities;

namespace Services.Contracts
{
    public interface ITournamentService
    {
        public Task<IEnumerable<TournamentDto>> GetAllAsync(bool includeGames);
        public Task<int> CountAsync();
        public Task<IEnumerable<PagingTournamentDto>> PagingAsync(int page, int pageSize, bool includeGames);
        public Task<TournamentDto> GetAsync(int id, bool includeGames = false);
        public Task<Status> Remove(int id);
        public Task<Status> Add(AddTournamentDto agDto);
    }

    public class Status
    {
        public string Message { get; set; }
    }
}

