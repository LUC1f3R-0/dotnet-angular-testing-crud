using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(new
        {
            message = "hello worrrrrrrrrrrrrld"
        });
    }

    [HttpPost]
    public async Task<IActionResult> PostUser([FromBody] User user)
    {
        return Ok(new
        {
            message= "gregre"
        });
    }
}