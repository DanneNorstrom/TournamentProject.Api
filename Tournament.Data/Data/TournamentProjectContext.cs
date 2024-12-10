using Microsoft.EntityFrameworkCore;
using TournamentProject.Core.Entities;

namespace TournamentProject.Data.Data
{
    public class TournamentProjectContext : DbContext
    {
        public TournamentProjectContext(DbContextOptions<TournamentProjectContext> options)
            : base(options)
        {
        }

        public DbSet<Tournament> Tournament { get; set; } = default!;
        public DbSet<Game> Game { get; set; } = default!;
    }
}
