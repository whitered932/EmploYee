using Microsoft.AspNetCore.Mvc;

namespace Startup.Controllers;

public class ProfileController : BaseController
{
    [HttpPost("getName")]
    public async Task<IActionResult> GetName()
    {
        return Ok("Admin");
    }
}