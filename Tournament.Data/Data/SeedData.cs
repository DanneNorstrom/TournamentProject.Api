using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentProject.Core.Entities;
using TournamentProject.Data.Data;

namespace TournamentProject.Data.Data
{
    public class SeedData
    {
        private static TournamentProjectApiContext context = default!;
        public static async Task Init(TournamentProjectApiContext _context)
        {
            context = _context;



            if (!_context.Tournament.Any())
            {
                var t1 = new Tournament
                {
                    Title = "First Tournament",
                    StartDate = DateTime.Now,
                };

                var g1t1 = new Game
                {
                    Title = "Chess",
                    Time = DateTime.Now,
                    TournamentId = 1
                };

                var g2t1 = new Game
                {
                    Title = "Pacman",
                    Time = DateTime.Now,
                    TournamentId = 1
                };

                var t2 = new Tournament
                {
                    Title = "Second Tournament",
                    StartDate = DateTime.Now,
                };

                var g1t2 = new Game
                {
                    Title = "Risk",
                    Time = DateTime.Now,
                    TournamentId = 2
                };

                var g2t2 = new Game
                {
                    Title = "Donkey Kong",
                    Time = DateTime.Now,
                    TournamentId = 2
                };

                
                _context.Tournament.Add(t1);
                _context.Tournament.Add(t2);

                await _context.SaveChangesAsync();

                _context.Game.Add(g1t1);
                _context.Game.Add(g2t1);
                _context.Game.Add(g1t2);
                _context.Game.Add(g2t2);

                await _context.SaveChangesAsync();
            }
        }
    }
}
      
