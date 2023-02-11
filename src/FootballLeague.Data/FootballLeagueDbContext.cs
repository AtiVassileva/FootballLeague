using FootballLeague.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.Data
{
    public class FootballLeagueDbContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }
    }
}