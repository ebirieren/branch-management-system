namespace Application.DTOs;

using Domain.Enums;

public record UserDTO
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public UserRoles Role { get; init;}
}