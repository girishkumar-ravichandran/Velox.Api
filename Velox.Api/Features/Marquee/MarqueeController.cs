using MediatR;
using Microsoft.AspNetCore.Mvc;
using Velox.Api.Features.Marquee.Commands;
using Velox.Api.Features.Marquee.Queries;

namespace Velox.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarqueeController : ControllerBase
    {

        private readonly IMediator _mediator;

        public MarqueeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Create Marquee
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateMarqueeCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var MarqueeDto = await _mediator.Send(command);
            return Ok(new { message = "Marquee created successfully", Marquee = MarqueeDto });
        }

        // Get Marquee by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var Marquee = await _mediator.Send(new GetMarqueeByIdQuery { MarqueeId = id });

                if (Marquee == null)
                    return NotFound(new { message = "Marquee not found" });

                return Ok(Marquee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // Update Marquee
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMarqueeCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            command.MarqueeId = id; // Set the ID for the Marquee to be updated
            var updatedMarquee = await _mediator.Send(command);

            if (updatedMarquee == null)
                return NotFound(new { message = "Marquee not found" });

            return Ok(new { message = "Marquee updated successfully", Marquee = updatedMarquee });
        }

        // Delete Marquee
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteMarqueeCommand { MarqueeId = id });

                if (!result)
                    return NotFound(new { message = "Marquee not found" });

                return Ok(new { message = "Marquee deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // Get All Marquees
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Marquees = await _mediator.Send(new GetAllMarqueesQuery());

                return Ok(Marquees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
