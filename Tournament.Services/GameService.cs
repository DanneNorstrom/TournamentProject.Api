using AutoMapper;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
