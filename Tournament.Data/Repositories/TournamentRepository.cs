using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentProject.Core.Entities;
using TournamentProject.Core.Repositories;
using TournamentProject.Data.Data;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace TournamentProject.Data.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentProjectApiContext _context;

        public TournamentRepository(TournamentProjectApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tournament>> GetAllAsync()
        { 
            return await _context.Tournament.ToListAsync();
        }

        public async Task<IEnumerable<Tournament>> GetAllAsync(bool includeGames)
        {
            if (includeGames)
            {
                return await _context.Tournament
                .Include(t => t.Games)
                .ToListAsync();
            }

            else
            {
                return await _context.Tournament
               .ToListAsync();
            }
        }

        public async Task<Tournament> GetAsync(int id)
        {
            return await _context.Tournament.FirstOrDefaultAsync(t => t.Id == id);
            //return await _context.Tournament.FindAsync(id);
        }



        public bool Any(int id)
        {
            return _context.Tournament.Any(t => t.Id == id);
        } 

        public void Add(Tournament tournament)
        {
            _context.Tournament.Add(tournament);
            //_context.SaveChangesAsync();
        }   

        public void Update(Tournament tournament)
        {
            //_context.Tournament.Update(tournament);
            _context.Entry(tournament).State = EntityState.Modified;
            //_context.SaveChangesAsync();
        }

        public void Remove(Tournament tournament)
        {
            _context.Tournament.Remove(tournament);
            //_context.SaveChangesAsync();
        }
    }
}
