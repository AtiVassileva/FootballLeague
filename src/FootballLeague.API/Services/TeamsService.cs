using FootballLeague.API.Services.Contracts;
using FootballLeague.Data;
using FootballLeague.Models;
using FootballLeague.Models.Request;
using FootballLeague.Models.Response;

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

        void ITeamsService.CreateTeam(TeamRequestModel model)
        {
            var teamToAdd = new Team()
            {
                Name = model.Name,
                Country = model.Country,
                Points = model.Points
            };

            _dbContext.Teams.Add(teamToAdd);
            _dbContext.SaveChanges();            
        }

        bool ITeamsService.DeleteTeam(Guid id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Team> ITeamsService.GetAllTeams()
        {
            throw new NotImplementedException();
        }

        TeamResponseModel ITeamsService.GetTeamById(Guid id)
        {
            throw new NotImplementedException();
        }

        int ITeamsService.GetTeamPoints(int teamId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Team> ITeamsService.GetTeamsRanking()
        {
            throw new NotImplementedException();
        }

        bool ITeamsService.UpdateTeam(Guid id, TeamRequestModel model)
        {
            throw new NotImplementedException();
        }

        void ITeamsService.UpdateTeamScore(Guid id, int pointsToAdd)
        {
            throw new NotImplementedException();
        }
    }
}