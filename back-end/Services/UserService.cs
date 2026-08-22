using backend.Exceptions;
using backend.Models;
using MyApp.DTOs;
using MyApp.Interfaces;

namespace MyApp.Services;

public class UserService(IUserRepository _userRepository) : IUserService
{
    public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
    {
        if (await _userRepository.GetByEmailAsync(dto.Email) is not null)
            throw new ConflictException("A user with this email already exists.");

        var user = new User
        {
            firstName = dto.FirstName,
            lastName = dto.LastName,
            email = dto.Email,
            age = dto.Age
        };
        var createUser = await _userRepository.CreateAsync(user);
        return ToDo(createUser);
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return [.. users.Select(ToDo)];
    }

    public async Task<UserDto> GetUserByIdAsync(Guid guid)
    {
        var user = await _userRepository.GetByUuidAsync(guid) ?? throw new NotFoundException("No User found");
        return ToDo(user);
    }

    public async Task<UserDto> DeleteUserByIdAsync(Guid guid)
    {
        var user = await _userRepository.GetByUuidAsync(guid) ?? throw new NotFoundException("No User found");
        return ToDo(user);
    }

    private static UserDto ToDo(User user)
    {
        return new UserDto
        {
            UuId = user.uuid,
            FirstName = user.firstName,
            LastName = user.lastName,
            Email = user.email,
            Age = user.age
        };
    }
}