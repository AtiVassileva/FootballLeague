using FootballLeague.Models;

namespace FootballLeague.API.Services.Contracts
{
    public interface ITeamsService
    {
        IEnumerable<Team> GetAllTeams();
    }
}