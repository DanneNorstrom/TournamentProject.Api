using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentProject.Core.Entities;
using TournamentProject.Core.Dto;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TournamentProject.Data.Data
{
    public class TournamentMappings : Profile
    {
        public TournamentMappings()
        {
            CreateMap<Tournament, TournamentDto>();
            CreateMap<Game, GameDto>();
            CreateMap<AddTournamentDto, Tournament>();
            CreateMap<UpdateTournamentDto, Tournament>();
            CreateMap<AddGameDto, Game>();
            CreateMap<UpdateGameDto, Game>();
        }
    }
}
