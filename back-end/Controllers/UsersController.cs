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
    public async Task<IActionResult> GetUser(Guid guid)
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
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto user)
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

    [HttpDelete("{guid:guid}")]
    public async Task<IActionResult> DeleteUser(Guid guid)
    {
        Console.WriteLine("starting");
        var user = await _userService.DeleteUserByIdAsync(guid);
        var resonse = new ApiResponse<UserDto>
        {
            Success = true,
            Message = "User Deleted Sucesspully",
            Data = user
        };
        Console.WriteLine("Success");
        return StatusCode(StatusCodes.Status200OK, resonse);
    }
}