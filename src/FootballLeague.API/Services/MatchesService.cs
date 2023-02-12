using AutoMapper;
using FootballLeague.API.Services.Contracts;
using FootballLeague.Data;
using FootballLeague.Models;
using FootballLeague.Models.Request;
using FootballLeague.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.API.Services
{
    public class MatchesService : IMatchesService
    {
        private readonly FootballLeagueDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITeamsService _teamsService;

        public MatchesService(FootballLeagueDbContext dbContext, IMapper mapper, ITeamsService teamsService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _teamsService = teamsService;
        }

        public async Task<Guid> CreateMatch(MatchRequestModel model)
        {
            try
            {
                var isHostTeamPresent = _teamsService.FindTeam(model.HostId);
                var isGuestTeamPresent = _teamsService.FindTeam(model.GuestId);
            }
            catch (ArgumentException)
            {
                throw;
            }
            var matchToAdd = _mapper.Map<Match>(model);
            await _dbContext.Matches.AddAsync(matchToAdd);
            await _dbContext.SaveChangesAsync();

            UpdateTeamsScore(matchToAdd.HostId, matchToAdd.GuestId, matchToAdd.HostGoals, matchToAdd.GuestGoals);

            return matchToAdd.Id;
        }

        public async Task<bool> DeleteMatch(Guid id)
        {
            var match = await FindMatchAsync(id);
            _dbContext.Matches.Remove(match);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MatchResponseModel>> GetAllMatches()
        {
            if (!_dbContext.Teams.Any())
            {
                throw new ArgumentException("No matches available!");
            }

            var matches = await _dbContext.Matches.ToListAsync();
            var matchesResponse = _mapper.Map<IEnumerable<MatchResponseModel>>(matches);
            return matchesResponse;
        }

        public async Task<MatchResponseModel> GetMatchById(Guid id)
        {
            var match = await FindMatchAsync(id);
            var matchResponse = _mapper.Map<MatchResponseModel>(match);
            return matchResponse;
        }

        public async Task<bool> UpdateMatch(Guid id, MatchEditModel model)
        {
            var match = await FindMatchAsync(id);

            match.PlayedOn = model.PlayedOn;
            match.HostGoals = model.HostGoals;
            match.GuestGoals = model.GuestGoals;

            _dbContext.SaveChanges();

            UpdateTeamsScore(match.HostId, match.GuestId, match.HostGoals, match.GuestGoals);

            return true;
        }

        private async Task<Match> FindMatchAsync(Guid id)
        {
            var match = await _dbContext.Matches.FirstOrDefaultAsync(t => t.Id == id);

            if (match == null)
            {
                throw new ArgumentException("Match does not exist!");
            }

            return match;
        }

        private void UpdateTeamsScore(Guid hostId, Guid guestId, int hostGoals, int guestGoals)
        {
            if (hostGoals == guestGoals)
            {
                _teamsService.UpdateTeamScore(hostId, 1);
                _teamsService.UpdateTeamScore(guestId, 1);
            }
            else if (hostGoals > guestGoals)
            {
                _teamsService.UpdateTeamScore(hostId, 3);
                _teamsService.UpdateTeamScore(guestId, 0);
            }
            else
            {
                _teamsService.UpdateTeamScore(hostId, 0);
                _teamsService.UpdateTeamScore(guestId, 3);
            }

            _dbContext.SaveChanges();
        }
    }
}