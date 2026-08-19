using backend.Models;

namespace MyApp.Interfaces;

public interface IUserRepository
{
    // Create a user
    Task<User> CreateAsync(User user);
    //Get A User By Id
    Task<User?> GetByUuidAsync(Guid guid);
    // Get a user by email
    Task<User?> GetByEmailAsync(string email);
    // Get All users
    Task<List<User>> GetAllAsync();

    // Task AddAsync(User user);
    //Delete User by Id
    Task RemoveAsync(User user);

    // void Remove(User user);
}