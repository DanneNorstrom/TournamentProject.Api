using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentProject.Core.Entities;

namespace TournamentProject.Core.Repositories
{
    public interface IGameRepository
    {
        public Task<IEnumerable<Game>> GetAllAsync();
        public Task<Game?> GetAsync(int id);
        public Task<bool> AnyAsync(int id);
        public void Add(Game tournament);
        public void Update(Game tournament);
        public void Remove(Game tournament);
    }
}
