using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Startup.Features.Stage;

namespace Startup.Controllers;

public class StageController(IMediator mediator) : BaseController
{
  
    [Authorize]
    [HttpPost("getMany")]
    public async Task<IActionResult> GetStages([FromBody] GetStagesQuery query)
    {
        var result = await mediator.Send(query);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }
        return Ok(result.Value);
    } 
    
    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> CreateStage([FromBody] CreateStageCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }
        return Ok();
    }
    
    [Authorize]
    [HttpPost("delete")]
    public async Task<IActionResult> DeleteStage([FromBody] DeleteStageCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }
        return Ok();
    }
}