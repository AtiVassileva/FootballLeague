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
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allTeams = await _teamsService.GetAllTeams();
                return Ok(allTeams);
            }
            catch (ArgumentException argExc)
            {
                return StatusCode(StatusCodes.Status400BadRequest, argExc.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"A server error occured while processing your request: {ex.Message}");
            }
        }

        [HttpGet("ranking")]
        public async Task<IActionResult> GetTeamsRanking()
        {
            try
            {
                var rankedTeams = await _teamsService.GetTeamsRanking();
                return Ok(rankedTeams);
            }
            catch (ArgumentException argExc)
            {
                return StatusCode(StatusCodes.Status400BadRequest, argExc.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"A server error occured while processing your request: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                var team = await _teamsService.GetTeamById(id);
                return Ok(team);
            }
            catch (ArgumentException argExc)
            {
                return StatusCode(StatusCodes.Status400BadRequest, argExc.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"A server error occured while processing your request: {ex.Message}");
            }
        }

        [HttpGet("{id}/score")]
        public async Task<IActionResult> GetTeamPoints([FromRoute] Guid id)
        {
            try
            {
                var teamPoints = await _teamsService.GetTeamPoints(id);
                return Ok(teamPoints);
            }
            catch (ArgumentException argExc)
            {
                return StatusCode(StatusCodes.Status400BadRequest, argExc.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"A server error occured while processing your request: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] TeamRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState.Values.SelectMany(s => s.Errors));
            }

            try
            {
                var isCreatedSuccessfully = await _teamsService.CreateTeam(model);
                return Ok(isCreatedSuccessfully);
            }
            catch (ArgumentException argExc)
            {
                return StatusCode(StatusCodes.Status400BadRequest, argExc.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"A server error occured while processing your request: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam([FromRoute] Guid id, [FromBody] TeamRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState.Values.SelectMany(s => s.Errors));
            }

            try
            {
                var isUpdatedSuccessfully = await _teamsService.CreateTeam(model);
                return Ok(isUpdatedSuccessfully);
            }
            catch (ArgumentException argExc)
            {
                return StatusCode(StatusCodes.Status400BadRequest, argExc.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"A server error occured while processing your request: {ex.Message}");
            }
        }

        [HttpPut("{id}/score")]
        public async Task<IActionResult> UpdateTeamPoints([FromRoute] Guid id, int pointsToAdd)
        {
            if (pointsToAdd < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Points cannot be a negative number!");
            }

            try
            {
                var updatedScore = await _teamsService.UpdateTeamScore(id, pointsToAdd);
                return Ok(updatedScore);
            }
            catch (ArgumentException argExc)
            {
                return StatusCode(StatusCodes.Status400BadRequest, argExc.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"A server error occured while processing your request: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam([FromRoute] Guid id)
        {
            try
            {
                var isDeletedSuccessfully = await _teamsService.DeleteTeam(id);
                return Ok(isDeletedSuccessfully);
            }
            catch (ArgumentException argExc)
            {
                return StatusCode(StatusCodes.Status400BadRequest, argExc.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"A server error occured while processing your request: {ex.Message}");
            }
        }
    }
}
