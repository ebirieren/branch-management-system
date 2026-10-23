using Domain.User;
using Application.Services;

namespace Application.Interfaces.Services;

public interface ITokenService
{
    string CreateToken(User user);
}