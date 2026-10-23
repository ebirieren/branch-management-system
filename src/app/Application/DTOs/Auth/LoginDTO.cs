namespace Application.DTOs;
public record LoginDTO
{
    public string Token { get; init; } = string.Empty;

    public DateTime ExpiresAt { get; init; }
}