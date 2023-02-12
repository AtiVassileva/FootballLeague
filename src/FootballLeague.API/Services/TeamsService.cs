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

        public void CreateTeam(TeamRequestModel model)
        {
            var teamToAdd = _mapper.Map<Team>(model);
            _dbContext.Teams.Add(teamToAdd);
            _dbContext.SaveChanges();            
        }

        public bool DeleteTeam(Guid id)
        {
            var team = _dbContext.Teams.FirstOrDefault(t => t.Id == id);

            if (team == null)
            {
                throw new ArgumentException("Team does not exist!");
            }

            _dbContext.Teams.Remove(team);
            _dbContext.SaveChanges();
            return true;
        }

        public IEnumerable<Team> GetAllTeams()
        {
            var teams = _dbContext.Teams.ToList();
            return teams;
        }

        public TeamResponseModel GetTeamById(Guid id)
        {
            var team = _dbContext.Teams.FirstOrDefault(t => t.Id == id);

            if (team == null)
            {
                throw new ArgumentException("Team does not exist!");
            }

            var teamResponse = _mapper.Map<TeamResponseModel>(team);
            return teamResponse;
        }

        public int GetTeamPoints(Guid teamId)
        {
            var team = _dbContext.Teams.FirstOrDefault(t => t.Id == teamId);

            if (team == null)
            {
                throw new ArgumentException("Team does not exist!");
            }

            return team.Points;
        }

        public IEnumerable<TeamPointsResponseModel> GetTeamsRanking()
        {
            var teams = GetAllTeams();

            if (!teams.Any())
            {
                throw new ArgumentException("No teams available!");
            }

            var rankedTeams = teams
                .OrderByDescending(t => t.Points)
                .ToList();

            var rankedTeamsResponse = _mapper.Map<IEnumerable<TeamPointsResponseModel>>(rankedTeams);

            return rankedTeamsResponse;
        }

        public bool UpdateTeam(Guid id, TeamRequestModel model)
        {
            var team = _dbContext.Teams.FirstOrDefault(t => t.Id == id);

            if (team == null)
            {
                throw new ArgumentException("Team does not exist!");
            }

            team.Name = model.Name;
            team.Country = model.Country;
            team.Points = model.Points;

            _dbContext.SaveChanges();

            return true;
        }

        public void UpdateTeamScore(Guid id, int pointsToAdd)
        {
            var team = _dbContext.Teams.FirstOrDefault(t => t.Id == id);

            if (team == null)
            {
                throw new ArgumentException("Team does not exist!");
            }

            team.Points += pointsToAdd;
            _dbContext.SaveChanges();
        }
    }
}