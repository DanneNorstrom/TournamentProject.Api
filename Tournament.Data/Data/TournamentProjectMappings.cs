using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentProject.Core.Entities;
using TournamentProject.Core.Dto;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TournamentProject.Data.Data
{
    public class TournamentProjectMappings : Profile
    {
        public TournamentProjectMappings()
        {
            CreateMap<Tournament, TournamentDto>();
            CreateMap<Tournament, PagingTournamentDto>();
            CreateMap<AddTournamentDto, Tournament>();
            CreateMap<UpdateTournamentDto, Tournament>();

            CreateMap<Game, GameDto>();
            CreateMap<Game, PagingGameDto>();
            CreateMap<AddGameDto, Game>();
            CreateMap<UpdateGameDto, Game>();
        }
    }
}
