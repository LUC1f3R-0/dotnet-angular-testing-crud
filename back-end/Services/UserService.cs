

using backend.Models;
using MyApp.DTOs;
using MyApp.Interfaces;

namespace MyApp.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
    {
        if (dto.Age < 18)
        {
            throw new ArgumentException("User must be at least 18 years old.");
        }

        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Age = dto.Age
        };

        var createUser = await _userRepository.CreateAsync(user);

        return new UserDto
        {
            Id = createUser.Id,
            FirstName = createUser.FirstName,
            LastName = createUser.LastName,
            Email = createUser.Email,
            Age = createUser.Age
        };
    }
}