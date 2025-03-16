using MediatR;
using Microsoft.AspNetCore.Mvc;
using Velox.Api.Features.Panelist.Commands;
using Velox.Api.Features.Panelist.Queries;

namespace Velox.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PanelistController : ControllerBase
    {

        private readonly IMediator _mediator;

        public PanelistController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Create Panelist
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreatePanelistCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var PanelistDto = await _mediator.Send(command);
            return Ok(new { message = "Panelist created successfully", Panelist = PanelistDto });
        }

        // Get Panelist by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var Panelist = await _mediator.Send(new GetPanelistByIdQuery { PanelistId = id });

                if (Panelist == null)
                    return NotFound(new { message = "Panelist not found" });

                return Ok(Panelist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // Update Panelist
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePanelistCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            command.PanelistId = id; // Set the ID for the Panelist to be updated
            var updatedPanelist = await _mediator.Send(command);

            if (updatedPanelist == null)
                return NotFound(new { message = "Panelist not found" });

            return Ok(new { message = "Panelist updated successfully", Panelist = updatedPanelist });
        }

        // Delete Panelist
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeletePanelistCommand { PanelistId = id });

                if (!result)
                    return NotFound(new { message = "Panelist not found" });

                return Ok(new { message = "Panelist deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // Get All Panelists
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Panelists = await _mediator.Send(new GetAllPanelistsQuery());

                return Ok(Panelists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
