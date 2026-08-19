using MyApp.DTOs;

namespace MyApp.Services;

public interface IUserService
{
    // Create User
    Task<UserDto> CreateUserAsync(CreateUserDto dto);
    // Get All Users
    Task<List<UserDto>> GetAllUsersAsync();
    // Get User By Id
    Task<UserDto> GetUserByIdAsync(Guid guid);
    // DELETE user
    Task<UserDto> DeleteUserByIdAsync(Guid guid);
}