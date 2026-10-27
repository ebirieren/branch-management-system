using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Interfaces.Services;
using Application.Requests;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
     _authenticationService = authenticationService;   
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<LoginDTO?>> CheckUserAccount(LoginRequest request)
    {
        LoginDTO? result = await _authenticationService.CheckUserAccount(request);

        if (result is null)
        {
            return Unauthorized("Invalid email or password.");
        }

        return Ok(result);
    }
}