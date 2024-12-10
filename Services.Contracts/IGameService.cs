using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentProject.Core.Dto;
using TournamentProject.Core.Entities;

namespace Services.Contracts
{
    public interface IGameService
    {
        public Task<GameDto> GetAsync(int id);
        public Task<int> CountAsync();
        public Task<IEnumerable<PagingGameDto>> PagingAsync(int page, int pageSize);
        public Task<Status> Remove(int id);
        public Task<Status> Add(AddGameDto agDto);
    }

    public class Status
    {
        public string Message { get; set; }
    }
}
