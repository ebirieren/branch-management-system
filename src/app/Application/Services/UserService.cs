using Application.DTOs;
using Application.Requests;
using Application.Interfaces;

namespace Application.Interfaces.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<UserDTO?> GetByIdAsync(Guid id)
    {
        return new UserDTO{};
    }

    public async Task<IReadOnlyList<UserDTO?>> GetAllAsync()
    {
        return null;
    }

    public async Task<UserDTO> CreateAsync(CreateUserRequest request)
    {
     return null;   
    }

    public async Task<UserDTO> UpdateAsync(UpdateUserRequest request)
    {
        return null;   
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return true;
    }
}