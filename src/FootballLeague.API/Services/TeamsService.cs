using FootballLeague.API.Services.Contracts;
using FootballLeague.Data;
using FootballLeague.Models;

namespace FootballLeague.API.Services
{
    public class TeamsService : ITeamsService
    {
        private readonly FootballLeagueDbContext _dbContext;

        public TeamsService(FootballLeagueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Team> GetAllTeams() => _dbContext.Teams.ToList();
    }
}