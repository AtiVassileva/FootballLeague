using FootballLeague.Models.Request;
using FootballLeague.Models.Response;

namespace FootballLeague.API.Services.Contracts
{
    public interface IMatchesService
    {
        Task<IEnumerable<MatchResponseModel>> GetAllMatches();
        Task<MatchResponseModel> GetMatchById(Guid id);
        Task<Guid> CreateMatch(MatchRequestModel model);
        Task<bool> UpdateMatch(Guid id, MatchEditModel model);
        Task<bool> DeleteMatch(Guid id);
    }
}