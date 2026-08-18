using backend.Exceptions;
using backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
        if(existingUser is not null)
        {
            throw new ConflictException("A user with this email already exists.");
        }
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

    private static UserDto ToDo(User user)
    {
        return new UserDto
        {
            Id = user.id,
            FirstName = user.firstName,
            LastName = user.lastName,
            Email = user.email,
            Age = user.age
        };
    }
}