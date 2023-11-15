using BasketService.API.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BasketService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpPost(Name = "UpdateItem")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> UpdateItem([FromBody] UpdateItemCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
