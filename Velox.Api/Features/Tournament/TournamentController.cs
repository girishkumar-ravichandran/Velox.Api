using MediatR;
using Microsoft.AspNetCore.Mvc;
using Velox.Api.Features.Tournament.Commands;
using Velox.Api.Features.Tournament.Queries;

namespace Velox.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TournamentController : ControllerBase
    {

        private readonly IMediator _mediator;

        public TournamentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Create Tournament
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateTournamentCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tournamentDto = await _mediator.Send(command);
            return Ok(new { message = "Tournament created successfully", tournament = tournamentDto });
        }

        // Get Tournament by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var tournament = await _mediator.Send(new GetTournamentByIdQuery { TournamentId = id });

                if (tournament == null)
                    return NotFound(new { message = "Tournament not found" });

                return Ok(tournament);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // Update Tournament
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTournamentCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            command.TournamentId = id; // Set the ID for the tournament to be updated
            var updatedTournament = await _mediator.Send(command);

            if (updatedTournament == null)
                return NotFound(new { message = "Tournament not found" });

            return Ok(new { message = "Tournament updated successfully", tournament = updatedTournament });
        }

        // Delete Tournament
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteTournamentCommand { TournamentId = id });

                if (!result)
                    return NotFound(new { message = "Tournament not found" });

                return Ok(new { message = "Tournament deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // Get All Tournaments
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tournaments = await _mediator.Send(new GetAllTournamentsQuery());

                return Ok(tournaments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
