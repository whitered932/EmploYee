using MediatR;
using Microsoft.AspNetCore.Mvc;
using Startup.Features.Achievement;
using Startup.Features.Employees;

namespace Startup.Controllers;

public class AchievementController(IMediator mediator) : BaseController
{
    [HttpPost("getOne")]
    public async Task<IActionResult> GetAchievement(GetAchievementQuery query)
    {
        var result = await mediator.Send(query);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }

        return Ok(result.Value);
    }

    [HttpPost("getMany")]
    public async Task<IActionResult> GetEmployees(GetAchievementsQuery query)
    {
        var result = await mediator.Send(query);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }

        return Ok(result.Value);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateAchievementCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpPost("delete")]
    public async Task<IActionResult> DeleteEmployee(DeleteAchievementCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateEmployee(UpdateAchievementCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.IsSuccessfull)
        {
            return BadRequest();
        }

        return Ok();
    }
}