using Application.DTOs;
using Application.Requests;
using Application.Interfaces;
using Domain.User;
using Domain.Enums;

namespace Application.Interfaces.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public UserService(IUserRepository userRepository, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }
    public async Task<UserDTO?> GetByIdAsync(Guid id)
    {
        User? user = await _userRepository.GetByIdAsync(id);

        if (user is null)
        {
            return null;
        }
        
        return new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
    }

    public async Task<IReadOnlyList<UserDTO?>> GetAllAsync()
    {
        List<User> users = await _userRepository.GetAllAsync();

        return users.Select(user => new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
        })
        .ToList();
    }

    public async Task<UserDTO> CreateAsync(CreateUserRequest request)
    {

        User user = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            Role = UserRoles.User
        };

        var hashedPassword = _passwordService.Hash(user, request.Password);

        user.PasswordHash = hashedPassword;

        await _userRepository.AddAsync(user);

        return new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
        };
    }

    public async Task<UserDTO?> UpdateAsync(UpdateUserRequest request)
    {
        User? user = await _userRepository.GetByIdAsync(request.Id);

        if (user is null)
        {
            return null;
        }

        if (request.Password != null)
        {
            var hashedPassword = _passwordService.Hash(user, request.Password);
            user.PasswordHash = hashedPassword;
        }

        if(request.Name != null)
        {
            user.Name = request.Name;
        }
        if(request.Email != null)
        {
            user.Email = request.Email;
        }

        return new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        User? user = await _userRepository.GetByIdAsync(id);

        if(user is null)
        {
            return false;
        }

        _userRepository.Remove(user);

        return true;
    }
}