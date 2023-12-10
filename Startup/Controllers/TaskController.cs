using MediatR;
using Microsoft.AspNetCore.Mvc;
using Startup.Features.Task;

namespace Startup.Controllers;

public class TaskController(IMediator mediator) : BaseController
{
    [HttpPost("getOne")]
    public async Task<IActionResult> GetTask(GetTaskQuery query)
    {
        var result = await mediator.Send(query);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }
        return Ok(result.Value);
    } 
    [HttpPost("getMany")]
    public async Task<IActionResult> GetTasks(GetTasksQuery query)
    {
        var result = await mediator.Send(query);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }
        return Ok(result.Value);
    } 
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateTask(CreateTaskCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }
        return Ok();
    }
    
    [HttpPost("delete")]
    public async Task<IActionResult> DeleteTask(DeleteTaskCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }
        return Ok();
    }
    
    [HttpPost("update")]
    public async Task<IActionResult> UpdateUpdate(UpdateTaskCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }
        return Ok();
    }
}