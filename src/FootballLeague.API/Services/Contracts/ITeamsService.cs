using FootballLeague.Models;
using FootballLeague.Models.Request;
using FootballLeague.Models.Response;

namespace FootballLeague.API.Services.Contracts
{
    public interface ITeamsService
    {
        IEnumerable<Team> GetAllTeams();
        IEnumerable<Team> GetTeamsRanking();
        TeamResponseModel GetTeamById(Guid id);
        int GetTeamPoints(int teamId);
        Guid CreateTeam(TeamRequestModel model);
        bool UpdateTeam(Guid id, TeamRequestModel model);
        void UpdateTeamScore(Guid id, int pointsToAdd);
        bool DeleteTeam(Guid id);
    }
}