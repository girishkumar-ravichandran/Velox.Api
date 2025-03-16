using MediatR;
using Microsoft.AspNetCore.Mvc;
using Velox.Api.Features.Sponser.Commands;
using Velox.Api.Features.Sponser.Queries;

namespace Velox.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SponserController : ControllerBase
    {

        private readonly IMediator _mediator;

        public SponserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Create Sponser
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateSponserCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var SponserDto = await _mediator.Send(command);
            return Ok(new { message = "Sponser created successfully", Sponser = SponserDto });
        }

        // Get Sponser by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var Sponser = await _mediator.Send(new GetSponserByIdQuery { SponserId = id });

                if (Sponser == null)
                    return NotFound(new { message = "Sponser not found" });

                return Ok(Sponser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // Update Sponser
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSponserCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            command.SponserId = id; // Set the ID for the Sponser to be updated
            var updatedSponser = await _mediator.Send(command);

            if (updatedSponser == null)
                return NotFound(new { message = "Sponser not found" });

            return Ok(new { message = "Sponser updated successfully", Sponser = updatedSponser });
        }

        // Delete Sponser
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteSponserCommand { SponserId = id });

                if (!result)
                    return NotFound(new { message = "Sponser not found" });

                return Ok(new { message = "Sponser deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // Get All Sponsers
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Sponsers = await _mediator.Send(new GetAllSponsersQuery());

                return Ok(Sponsers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
