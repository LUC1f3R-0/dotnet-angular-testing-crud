using backend.Data;
using backend.Models;
using MyApp.Interfaces;

namespace MyApp.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        Console.WriteLine("testing");
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }
}