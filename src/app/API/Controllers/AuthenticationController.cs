using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Interfaces.Services;
using Application.Requests;

namespace API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    public AuthenticationController()
    {
        
    }

    [HttpPost]
    public async Task<ActionResult<LoginDTO>> CheckUserAccount(LoginRequest request)
    {
        LoginDTO token = await 
        return Ok()
    }
}