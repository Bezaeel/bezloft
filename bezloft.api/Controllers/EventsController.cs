using bezloft.application.Common.Models;
using bezloft.application.Features.Events.Commands;
using bezloft.application.Features.Events.Queries;
using bezloft.application.Features.RSVPs.Commands;
using bezloft.core.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bezloft.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : Controller
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// This endpoint will fetch all events
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(BaseResponse<PagedModel<Event>>), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> all([FromQuery] GetAllEventQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// This endpoint will fetch event details
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(BaseResponse<GetEventDetailsResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{eventId}")]
        public async Task<IActionResult> details([FromRoute]Guid eventId)
        {
            var query = new GetEventDetailsQuery
            {
                Id = eventId,
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// This endpoint will edit event details
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(BaseResponse<BaseResponse>), StatusCodes.Status200OK)]
        [HttpPut]
        [Route("{eventId}")]
        public async Task<IActionResult> edit([FromRoute] Guid eventId, [FromForm]PatchEventCommandDTO dto)
        {
            var command = new PatchEventCommandCommand
            {
                Id = eventId,
                Description = dto.description,
                Visibility = dto.visibility,
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// This endpoint will invite participant to event
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(BaseResponse<BaseResponse>), StatusCodes.Status200OK)]
        [HttpPost]
        [Route("{eventId}")]
        public async Task<IActionResult> invite([FromRoute] Guid eventId, [FromBody] InviteDTO dto)
        {
            var command = new InviteParticipantCommand
            {
                EventId = eventId,
                UserId = dto.userId,
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// This endpoint will fetch all event attendees
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(BaseResponse<List<GetEventParticipantsResponseDTO>>), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{eventId}/attendees")]
        public async Task<IActionResult> attendees([FromRoute] Guid eventId)
        {
            var query = new GetEventParticipantsQuery
            {
                Id = eventId,
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
