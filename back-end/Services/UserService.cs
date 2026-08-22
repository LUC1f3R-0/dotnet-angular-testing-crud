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

        var createdUser = await _userRepository.CreateAsync(user);

        return ToDto(createdUser);
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return [.. users.Select(ToDto)];
    }

    public async Task<UserDto> GetUserByIdAsync(Guid guid)
    {
        var user = await _userRepository.GetByUuidAsync(guid)
            ?? throw new NotFoundException("No User found");

        return ToDto(user);
    }

    public async Task<UserDto> DeleteUserByIdAsync(Guid guid)
    {
        var user = await _userRepository.GetByUuidAsync(guid)
            ?? throw new NotFoundException("No User found");

        await _userRepository.RemoveAsync(user);

        return ToDto(user);
    }

    public async Task<UserDto> UpdateUserByIdAsync(Guid guid, CreateUserDto dto)
    {
        var existingUser = await _userRepository.GetByUuidAsync(guid)
            ?? throw new NotFoundException("User not found.");

        existingUser.firstName = dto.FirstName;
        existingUser.lastName = dto.LastName;
        existingUser.email = dto.Email;
        existingUser.age = dto.Age;

        await _userRepository.UpdateAsync(existingUser);

        return ToDto(existingUser);
    }

    private static UserDto ToDto(User user)
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