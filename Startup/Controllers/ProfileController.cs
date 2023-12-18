using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Startup.Controllers;

public class ProfileController : BaseController
{
    [Authorize]
    [HttpPost("getName")]
    public async Task<IActionResult> GetName()
    {
        return Ok("Admin");
    }
}