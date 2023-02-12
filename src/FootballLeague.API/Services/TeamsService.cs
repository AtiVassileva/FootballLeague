using AutoMapper;
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
        private readonly IMapper _mapper;

        public TeamsService(FootballLeagueDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<Team> GetAllTeams() => _dbContext.Teams.ToList();

        void ITeamsService.CreateTeam(TeamRequestModel model)
        {
            var teamToAdd = _mapper.Map<Team>(model);
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