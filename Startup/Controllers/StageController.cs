using MediatR;
using Microsoft.AspNetCore.Mvc;
using Startup.Features.Stage;

namespace Startup.Controllers;

public class StageController(IMediator mediator) : BaseController
{
  
    [HttpPost("getMany")]
    public async Task<IActionResult> GetStages(GetStagesQuery query)
    {
        var result = await mediator.Send(query);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }
        return Ok(result.Value);
    } 
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateStage(CreateStageCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }
        return Ok();
    }
    
    [HttpPost("delete")]
    public async Task<IActionResult> DeleteStage(DeleteStageCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }
        return Ok();
    }
}