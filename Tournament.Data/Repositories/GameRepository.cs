﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentProject.Core.Dto;
using TournamentProject.Core.Entities;
using TournamentProject.Core.Repositories;
using TournamentProject.Data.Data;

namespace TournamentProject.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly TournamentProjectContext _context;

        public GameRepository(TournamentProjectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Game.ToListAsync();
        }
        public async Task<IEnumerable<Game>> GetAllAsync(string title)
        {
            return await _context.Game.
                Where(g => g.Title == title).
                ToListAsync();
        }

        public async Task<Game> GetAsync(int id)
        {
            //return await _context.Game.FirstOrDefaultAsync(g => g.Id == id);
            return await _context.Game.FindAsync(id);
        }

        public bool Any(int id)
        {
            return _context.Game.Any(g => g.Id == id);
        }

        public void Add(Game game)
        {
            _context.Game.Add(game);
            //_context.SaveChangesAsync();
        }

        public void Update(Game game)
        {
            //_context.Game.Update(game);
            _context.Entry(game).State = EntityState.Modified;
            //_context.SaveChangesAsync();
        }

        public void Remove(Game game)
        {
            _context.Game.Remove(game);
            //_context.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Game.CountAsync();
        }
        public async Task<IEnumerable<Game>> PagingAsync(int page, int pageSize)
        {
            return await _context.Game
               .OrderBy(g => g.Id)
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();
        }
    }
}

