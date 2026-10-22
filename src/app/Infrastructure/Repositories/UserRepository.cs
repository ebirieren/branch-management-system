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

    public Task<User?> GetByEmailAsync(string email)
    {
        return _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email == email);
    }
}