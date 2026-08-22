using backend.Models;

namespace MyApp.Interfaces;

public interface IUserRepository
{
    Task<User> CreateAsync(User user);

    Task<User?> GetByUuidAsync(Guid guid);

    Task<User?> GetByEmailAsync(string email);

    Task<List<User>> GetAllAsync();

    Task RemoveAsync(User user);

    Task<User> UpdateAsync(User user);
}