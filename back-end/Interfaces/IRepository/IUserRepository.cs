using backend.Models;

namespace MyApp.Interfaces;

public interface IUserRepository
{
    Task<User> CreateAsync(User user);
}