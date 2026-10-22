using Application.DTOs;
using Application.Requests;
using Application.Services;

namespace Application.Interfaces.Services;

public interface ITokenService
{
    Task<LoginDTO?> CreateToken(LoginRequest request);
}