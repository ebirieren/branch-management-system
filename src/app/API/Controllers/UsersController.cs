using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Interfaces.Services;
using Application.Requests;

namespace API.Controllers;

[ApiController]
[Route("api/users")]

public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<UserDTO>> GetById(Guid id)
    {
        UserDTO? user = await _userService.GetByIdAsync(id);

        if(user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UserDTO>>> GetAll()
    {
        IReadOnlyList<UserDTO> users = await _userService.GetAllAsync();

        return Ok(users);
    }
    
    [HttpPost()]
    public async Task<ActionResult<UserDTO>> Create(CreateUserRequest request)
    {
        UserDTO user = await _userService.CreateAsync(request);

        return Ok(user);
    }

    [HttpPatch()]
    public async Task<ActionResult<UserDTO>> Update(UpdateUserRequest request)
    {
        UserDTO user = await _userService.UpdateAsync(request);
        
        return Ok(user);
    }

    [HttpDelete("{id:Guid}")]

    public async Task<ActionResult<bool>> Remove(Guid id)
    {
        bool result = await _userService.DeleteAsync(id);

        if (result == false)
        {
            return NotFound();
        }

        return Ok(result);
    }
}