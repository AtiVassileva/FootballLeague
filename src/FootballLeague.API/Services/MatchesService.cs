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

        public MatchesService(FootballLeagueDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> CreateMatch(MatchRequestModel model)
        {
            var matchToAdd = _mapper.Map<Match>(model);
            await _dbContext.Matches.AddAsync(matchToAdd);
            await _dbContext.SaveChangesAsync();
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
    }
}