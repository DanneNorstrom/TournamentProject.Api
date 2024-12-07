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
    }
}

