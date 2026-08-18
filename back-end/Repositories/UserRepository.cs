using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
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
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.email == email);
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUserById(Guid guid)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.uuid == guid);
    }
}