using FootballLeague.API.Services.Contracts;
using FootballLeague.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace FootballLeague.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchesService _matchesService;

        public MatchesController(IMatchesService matchesService)
        {
            _matchesService = matchesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allMatches = await _matchesService.GetAllMatches();
                return Ok(allMatches);
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
                var match = await _matchesService.GetMatchById(id);
                return Ok(match);
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
        public async Task<IActionResult> CreateMatch([FromBody] MatchRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState.Values.SelectMany(s => s.Errors));
            }

            try
            {
                var isCreatedSuccessfully = await _matchesService.CreateMatch(model);
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
        public async Task<IActionResult> UpdateMatch([FromRoute] Guid id, [FromBody] MatchEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState.Values.SelectMany(s => s.Errors));
            }

            try
            {
                var isUpdatedSuccessfully = await _matchesService.UpdateMatch(id, model);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch([FromRoute] Guid id)
        {
            try
            {
                var isDeletedSuccessfully = await _matchesService.DeleteMatch(id);
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