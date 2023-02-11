using FootballLeague.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.Data
{
    public class FootballLeagueDbContext : DbContext
    {
        public FootballLeagueDbContext()
        {
        }

        public FootballLeagueDbContext(DbContextOptions<FootballLeagueDbContext> options)
            : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>()
            .HasOne<Team>(m => m.Host)
            .WithMany(t => t.HomeMatches)
            .HasForeignKey(m => m.HostId);

            modelBuilder.Entity<Match>()
                .HasOne<Team>(m => m.Guest)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(m => m.GuestId);

            base.OnModelCreating(modelBuilder);
        }
    }
}