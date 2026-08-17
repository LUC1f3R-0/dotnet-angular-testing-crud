using backend.Models;
using Microsoft.AspNetCore.Mvc;
using MyApp.DTOs;
using MyApp.Services;

namespace backend.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {        
        return Ok(new
        {
            message = "hello worrrrrrrrrrrrrld"
        });
    }

    [HttpPost]
    public async Task<IActionResult> PostUser([FromBody] CreateUserDto user)
    {
        var createdUser = await _userService.CreateUserAsync(user);

        return Ok(new
        {
            user
        });
    }
}