using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Requests;
using Domain.User;

namespace Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordService _passwordService;

    public AuthenticationService(IUserRepository userRepository, ITokenService tokenService, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _passwordService = passwordService;
    }
    public async Task<LoginDTO?> CheckUserAccount(LoginRequest request)
    {
        User? user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            return null;
        }

        bool passwordIsCorrect = _passwordService.Verify(user, request.Password, user.PasswordHash);

        if (!passwordIsCorrect)
        {
            return null;
        }

        string token = _tokenService.CreateToken(user);

        return new LoginDTO
        {
            Token = token
        };
    }
}