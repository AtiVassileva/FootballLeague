using FootballLeague.API.Services.Contracts;
using FootballLeague.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace FootballLeague.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamsService _teamsService;

        public TeamsController(ITeamsService teamsService)
        {
            _teamsService = teamsService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_teamsService.GetAllTeams());

        [HttpGet("ranking")]
        public IActionResult GetTeamsRanking() => Ok(_teamsService.GetTeamsRanking());

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]Guid id)
        {
            var team = _teamsService.GetTeamById(id);
            return Ok(team);
        }
        
        [HttpGet("{id}/score")]
        public IActionResult GetTeamPoints([FromRoute]Guid id)
        {
            var teamPoints = _teamsService.GetTeamPoints(id);
            return Ok(teamPoints);
        }

        [HttpPost]
        public IActionResult CreateTeam([FromBody] TeamRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _teamsService.CreateTeam(model);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTeam([FromRoute] Guid id, [FromBody] TeamRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _teamsService.UpdateTeam(id, model);
            return Ok();
        }

        [HttpPut("{id}/score")]
        public IActionResult UpdateTeamPoints([FromRoute] Guid id, int pointsToAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _teamsService.UpdateTeamScore(id, pointsToAdd);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeam([FromRoute] Guid id)
        {
            var result = _teamsService.DeleteTeam(id);
            return Ok(result);
        }
    }
}
