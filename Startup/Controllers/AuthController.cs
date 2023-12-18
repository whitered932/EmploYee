using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Startup.Features.Auth;

namespace Startup.Controllers;

public class AuthController(IMediator mediator) : BaseController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }

        return Ok();
    }

    [Authorize]
    [HttpPost("profile")]
    public async Task<ActionResult<AccountInfoDto>> Profile([FromBody] GetProfileQuery query)
    {
        var result = await mediator.Send(query);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }

        return Ok(result.Value);
    }
}