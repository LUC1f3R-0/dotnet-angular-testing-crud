using MyApp.DTOs;

namespace MyApp.Services;

public interface IUserService
{
    Task<UserDto> CreateUserAsync(CreateUserDto dto);
}