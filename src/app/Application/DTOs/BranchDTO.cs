namespace Application.DTOs;
using Application.DTOs;

public record BranchDTO
{
    public int Id { get; init; }
    public string BranchName { get; init; } = string.Empty;
    public ICollection<BranchProductDTO> Products { get; init; } = new List<BranchProductDTO>();
    public Guid InvoiceId { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public DateTime CreatedAt { get; init; }
}
