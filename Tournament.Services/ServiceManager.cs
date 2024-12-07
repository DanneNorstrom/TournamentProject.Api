﻿using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static Tournament.Services.ServiceManager;

namespace TournamentProject.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ITournamentService> tournamentService;
        private readonly Lazy<IGameService> gameService;

        public ITournamentService TournamentService => tournamentService.Value;
        public IGameService GameService => gameService.Value;


        public ServiceManager(Lazy<ITournamentService> tournamentservice, Lazy<IGameService> gameservice)
        {
            tournamentService = tournamentservice;
            gameService = gameservice;
        }
    }
}
