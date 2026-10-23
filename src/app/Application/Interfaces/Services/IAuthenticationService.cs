using Application.DTOs;
using Application.Requests;

namespace Application.Interfaces.Services;
public interface IAuthenticationService
{
    Task<LoginDTO?> CheckUserAccount(LoginRequest request);
}