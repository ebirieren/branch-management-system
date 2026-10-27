using Microsoft.EntityFrameworkCore;
using Domain.User;
using Application.Interfaces;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDBContext _context;

    public UserRepository(AppDBContext context)
    {
        _context = context;
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        return _context.Users
            .FirstOrDefaultAsync(user => user.Id == id);
    }

    public Task<List<User>> GetAllAsync()
    {
        return _context.Users.ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();   
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void Remove(User user)
    {
        _context.Users.Remove(user);
        _context.SaveChanges();
    }
    public Task<User?> GetByEmailAsync(string email)
    {
        return _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email == email);
    }
}