using AutoMapper;
using FootballLeague.API.Services.Contracts;
using FootballLeague.Data;
using FootballLeague.Models.Data;
using FootballLeague.Models.Request;
using FootballLeague.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.API.Services
{
    using static Common.ExceptionMessages;
    public class TeamsService : ITeamsService
    {
        private readonly FootballLeagueDbContext _dbContext;
        private readonly IMapper _mapper;

        public TeamsService(FootballLeagueDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> CreateTeam(TeamRequestModel model)
        {
            var teamToAdd = _mapper.Map<Team>(model);
            await _dbContext.Teams.AddAsync(teamToAdd);
            await _dbContext.SaveChangesAsync();
            return teamToAdd.Id;
        }

        public async Task<bool> DeleteTeam(Guid id)
        {
            var team = await FindTeam(id);
            _dbContext.Teams.Remove(team);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TeamResponseModel>> GetAllTeams()
        {
            if (!_dbContext.Teams.Any())
            {
                throw new ArgumentException(NoTeamsAvailableExceptionMessage);
            }

            var teams = await _dbContext.Teams.ToListAsync();
            var teamsResponse = _mapper.Map<IEnumerable<TeamResponseModel>>(teams);
            return teamsResponse;
        }

        public async Task<TeamResponseModel> GetTeamById(Guid id)
        {
            var team = await FindTeam(id);
            var teamResponse = _mapper.Map<TeamResponseModel>(team);
            return teamResponse;
        }

        public async Task<int> GetTeamPoints(Guid teamId)
        {
            var team = await FindTeam(teamId);
            return team.Points;
        }

        public async Task<IEnumerable<TeamPointsResponseModel>> GetTeamsRanking()
        {
            var teams = await GetAllTeams();

            if (!teams.Any())
            {
                throw new ArgumentException(NoTeamsAvailableExceptionMessage);
            }

            var rankedTeams = teams
                .OrderByDescending(t => t.Points)
                .ThenBy(t => t.Name)
                .ToList();

            var rankedTeamsResponse = _mapper.Map<IEnumerable<TeamPointsResponseModel>>(rankedTeams);

            return rankedTeamsResponse;
        }

        public async Task<bool> UpdateTeam(Guid id, TeamEditModel model)
        {
            var team = await FindTeam(id);

            _dbContext.Entry(team).CurrentValues.SetValues(model);
            _dbContext.SaveChanges();

            return true;
        }

        public async Task<int> UpdateTeamScore(Guid id, int pointsToAdd)
        {
            var team = await FindTeam(id);
            team.Points += pointsToAdd;
            _dbContext.SaveChanges();
            return team.Points;
        }

        public async Task<Team> FindTeam(Guid id)
        {
            var team = await _dbContext.Teams.FirstOrDefaultAsync(t => t.Id == id);

            if (team == null)
            {
                throw new ArgumentException(NonExistingTeamExceptionMessage);
            }

            return team;
        }
    }
}