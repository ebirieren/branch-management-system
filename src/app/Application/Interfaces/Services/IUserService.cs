using Application.DTOs;
using Application.Requests;
using Application.Services;

namespace Application.Interfaces.Services;

public interface IUserService : IService<UserDTO, Guid, CreateUserRequest, UpdateUserRequest>
{
    
}