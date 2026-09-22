namespace Application.DTOs;

using Domain.Products;
public record BranchDTO
{
    public int Id { get; init; }
    public string BranchName { get; init; } = string.Empty;
    public ICollection<Product> Products { get; init; } = new List<Product>();
    public Guid InvoiceId { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public DateTime CreatedAt { get; init; }
}
