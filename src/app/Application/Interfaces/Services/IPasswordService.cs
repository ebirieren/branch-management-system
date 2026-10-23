using Application.DTOs;
using Application.Requests;
using Application.Services;
using Domain.User;

namespace Application.Interfaces.Services;

public interface IPasswordService
{
    bool Verify(User user, string passwordHash, string storedPasswordHash);

    string Hash(User user,string password);
}