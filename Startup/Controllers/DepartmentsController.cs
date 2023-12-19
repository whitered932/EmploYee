using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Startup.Features.Department;
using Startup.Features.Meeting;

namespace Startup.Controllers;

public class DepartmentsController(IMediator mediator) : BaseController
{
    [Authorize]
    [HttpPost("getMany")]
    public async Task<IActionResult> GetDepartment([FromBody] GetDepartmentsQuery query)
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
    public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentCommand command)
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
    public async Task<IActionResult> DeleteDepartment([FromBody] DeleteDepartmentCommand command)
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
    public async Task<IActionResult> UpdateDepartment([FromBody] UpdateDepartmentCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }
        return Ok();
    } 
}