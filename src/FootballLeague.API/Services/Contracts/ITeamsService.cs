using FootballLeague.Models.Request;
using FootballLeague.Models.Response;

namespace FootballLeague.API.Services.Contracts
{
    public interface ITeamsService
    {
        Task<IEnumerable<TeamResponseModel>> GetAllTeams();
        Task<IEnumerable<TeamPointsResponseModel>> GetTeamsRanking();
        Task<TeamResponseModel> GetTeamById(Guid id);
        Task<int> GetTeamPoints(Guid teamId);
        Task<Guid> CreateTeam(TeamRequestModel model);
        Task<bool> UpdateTeam(Guid id, TeamRequestModel model);
        Task<int> UpdateTeamScore(Guid id, int pointsToAdd);
        Task<bool> DeleteTeam(Guid id);
    }
}