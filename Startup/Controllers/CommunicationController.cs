using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Startup.Features.Communication;

namespace Startup.Controllers;

public class CommunicationController(IMediator mediator) : BaseController
{
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Get([FromBody] GetCommunicationsQuery query)
    {
        var result = await mediator.Send(query);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }

        return Ok(result.Value);
    }
}