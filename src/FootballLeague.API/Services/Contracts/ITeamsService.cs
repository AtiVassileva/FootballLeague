using FootballLeague.Models;
using FootballLeague.Models.Request;
using FootballLeague.Models.Response;

namespace FootballLeague.API.Services.Contracts
{
    public interface ITeamsService
    {
        IEnumerable<Team> GetAllTeams();
        IEnumerable<TeamPointsResponseModel> GetTeamsRanking();
        TeamResponseModel GetTeamById(Guid id);
        int GetTeamPoints(Guid teamId);
        void CreateTeam(TeamRequestModel model);
        bool UpdateTeam(Guid id, TeamRequestModel model);
        void UpdateTeamScore(Guid id, int pointsToAdd);
        bool DeleteTeam(Guid id);
    }
}