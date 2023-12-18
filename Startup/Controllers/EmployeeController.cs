using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Startup.Features.Employees;

namespace Startup.Controllers;

public class EmployeeController(IMediator mediator) : BaseController
{
    [Authorize]
    [HttpPost("getOne")]
    public async Task<IActionResult> GetEmployee([FromBody] GetEmployeeQuery query)
    {
        var result = await mediator.Send(query);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }
        return Ok(result.Value);
    }
    
    [Authorize]
    [HttpPost("info")]
    public async Task<IActionResult> GetEmployee([FromBody] GetEmployeeMainInfoQuery query)
    {
        var result = await mediator.Send(query);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }
        return Ok(result.Value);
    }
    
    [HttpPost("getMany")]
    public async Task<IActionResult> GetEmployees(GetEmployeesQuery query)
    {
        var result = await mediator.Send(query);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }
        return Ok(result.Value);
    } 
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }
        return Ok();
    }
    
    [HttpPost("delete")]
    public async Task<IActionResult> DeleteEmployee(DeleteEmployeeCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }
        return Ok();
    }
    
    // [HttpPost("update")]
    // public async Task<IActionResult> UpdateEmployee(UpdateEmployeeCommand command)
    // {
    //     var result = await mediator.Send(command);
    //     if (!result.IsSuccessfull)
    //     {
    //         return BadRequest();
    //     }
    //     return Ok();
    // }
}