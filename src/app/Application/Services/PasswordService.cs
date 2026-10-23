using Domain.User;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

public sealed class PasswordService : IPasswordService
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public PasswordService(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }
    public bool Verify(User user, string providedPassword, string storedPasswordHash)
    {
        PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, storedPasswordHash, providedPassword);

        return result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded;
    }

    public string Hash(User user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }
}