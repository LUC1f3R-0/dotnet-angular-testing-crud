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
        var users = await _userService.GetAllUsersAsync();
        var response = new ApiResponse<List<UserDto>>
        {
            Success = true,
            Message = "Users retrieved successfully.",
            Data = users
        };
        return StatusCode(StatusCodes.Status200OK, response);
    }

    [HttpGet("{guid:guid}")]
    public async Task<IActionResult> GetUserById( Guid guid)
    {
        var user = await _userService.GetUserByIdAsync(guid);
        var response = new ApiResponse<UserDto>
        {
            Success = true,
            Message = "User Recieved",
            Data = user
        };
        return StatusCode(StatusCodes.Status200OK, response);
    }

    [HttpPost]
    public async Task<IActionResult> PostUser([FromBody] CreateUserDto user)
    {
        var createdUser = await _userService.CreateUserAsync(user);
        var response = new ApiResponse<UserDto>
        {
            Success = true,
            Message = "User created successfully.",
            Data = createdUser
        };
        return StatusCode(StatusCodes.Status201Created, response); 
    }
}