using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Startup.Features.Task;

namespace Startup.Controllers;

public class TaskController(IMediator mediator) : BaseController
{
    [Authorize]
    [HttpPost("getOne")]
    public async Task<IActionResult> GetTask([FromBody] GetTaskQuery query)
    {   
        var result = await mediator.Send(query);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }
        return Ok(result.Value);
    } 
    [Authorize]
    [HttpPost("getMany")]
    public async Task<IActionResult> GetTasks([FromBody] GetTasksQuery query)
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
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
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
    public async Task<IActionResult> DeleteTask([FromBody] DeleteTaskCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }
        return Ok();
    }
    
    [Authorize]
    [HttpPost("update")]
    public async Task<IActionResult> UpdateUpdate([FromBody] UpdateTaskCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }
        return Ok();
    }
}