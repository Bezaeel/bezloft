using bezloft.application.Features.Events.Commands;
using bezloft.application.Features.Events.Queries;
using bezloft.application.Features.Profile.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bezloft.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// This endpoint will create an event
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        [Route("{userId}/event")]
        public async Task<IActionResult> Add([FromRoute]Guid userId, [FromBody]CreateEventDTO dto)
        {
            var command = new CreateEventCommand
            {
                Name = dto.name,
                Description = dto.description,
                Limit = dto.limit,
                Visibility = dto.visibility,
                ContactPersonId = userId,
                End = dto.end,
                Start = dto.start,
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// This endpoint will fetch all users events
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("{userId}/events")]
        public async Task<IActionResult> events([FromRoute] Guid userId, [FromQuery]GetContactPersonEventsDTO dto)
        {
            var query = new GetContactPersonEventsQuery
            {
                Id = userId,
                filter = dto.filter,
                Limit = dto.limit,
                Page = dto.page,
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
