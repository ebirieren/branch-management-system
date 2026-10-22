using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Requests;
using Domain.User;


namespace Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public TokenService(IConfiguration configuration, IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public async Task<LoginDTO?> CreateToken(LoginRequest request)
    {
        User? user = await _userRepository.GetByEmailAsync(request.Email);
        
        if (user is null)
        {
            return null;
        }

        return new LoginDTO
        {
            Token = "temporary-token"
        };
    }
}
