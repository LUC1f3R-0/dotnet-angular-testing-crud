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

    // Get All users
    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    // Get A User By Uuid
    public async Task<User?> GetByUuidAsync(Guid uuid)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.uuid == uuid);
    }

    // public void Remove(User user)
    // {
    //     _context.Users.Remove(user);
    // }

    // public async Task AddAsync(User user)
    // {
    //     await _context.Users.AddAsync(user);
    // }

    // public async Task RemoveAsync(User user)
    // {
    //     _context.Users.Remove(user);
    //     await _context.SaveChangesAsync();
    // }
    public async Task RemoveAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}