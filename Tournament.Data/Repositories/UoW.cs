using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentProject.Core.Repositories;
using TournamentProject.Data.Data;

namespace TournamentProject.Data.Repositories
{
    public class UoW : IUoW
    {
        private readonly TournamentProjectApiContext _context;

        public UoW(TournamentProjectApiContext context)
        {
            _context = context;
            TournamentRepository = new TournamentRepository(_context);
            GameRepository = new GameRepository(_context);
        }
        public ITournamentRepository TournamentRepository { get; } 
        public IGameRepository GameRepository { get; }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
