using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentProject.Core.Dto;
using TournamentProject.Core.Entities;

namespace TournamentProject.Core.Repositories
{
    public interface IGameRepository
    {
        public Task<IEnumerable<Game>> GetAllAsync();
        public Task<IEnumerable<Game>> GetAllAsync(string title);
        public Task<Game> GetAsync(int id);
        public bool Any(int id);
        public void Add(Game game);
        public void Update(Game game);
        public void Remove(Game game);
        public Task<int> CountAsync();
        public Task<IEnumerable<Game>> PagingAsync(int page, int pageSize);
    }
}
