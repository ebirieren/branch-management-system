using Domain.User;

namespace Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
}