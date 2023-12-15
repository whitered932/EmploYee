using MediatR;
using Microsoft.AspNetCore.Mvc;
using Startup.Features.Employees;
using Startup.Features.Meeting;

namespace Startup.Controllers;

public class MeetingsController(IMediator mediator) : BaseController
{
    // [HttpPost("getOne")]
    // public async Task<IActionResult> GetEmployee([FromBody] GetEmployeeQuery query)
    // {
    //     var result = await mediator.Send(query);
    //     if (!result.IsSuccessfull)
    //     {
    //         return BadRequest();
    //     }
    //     return Ok(result.Value);
    // }
    
    [HttpPost("getMany")]
    public async Task<IActionResult> GetEmployees(GetMeetingsQuery query)
    {
        var result = await mediator.Send(query);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }
        return Ok(result.Value);
    } 
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateMeetingCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }
        return Ok();
    }
    
    // [HttpPost("delete")]
    // public async Task<IActionResult> DeleteEmployee(DeleteEmployeeCommand command)
    // {
    //     var result = await mediator.Send(command);
    //     if (!result.IsSuccessfull)
    //     {
    //         return BadRequest();
    //     }
    //     return Ok();
    // }
}