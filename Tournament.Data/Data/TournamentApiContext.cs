using Microsoft.EntityFrameworkCore;
using TournamentProject.Core.Entities;

namespace TournamentProject.Data.Data
{
    public class TournamentProjectApiContext : DbContext
    {
        public TournamentProjectApiContext(DbContextOptions<TournamentProjectApiContext> options)
            : base(options)
        {
        }

        public DbSet<Tournament> Tournament { get; set; } = default!;
        public DbSet<Game> Game { get; set; } = default!;
    }
}
