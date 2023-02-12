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
    }
}
