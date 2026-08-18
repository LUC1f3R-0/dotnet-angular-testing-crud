using backend.Models;

namespace MyApp.Interfaces;

public interface IUserRepository
{
    Task<User> CreateAsync(User user);

    Task<User?> GetByEmailAsync(string email);
}