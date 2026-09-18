namespace Application.DTOs;

namespace Domain.Products;
public record BranchDTO
{
    public int Id { get; init; }
    public string BranchName { get; init; }
    public Guid ProductId { get; init; }
    public ICollection<Product?> Products { get; init; }
    public Guid InvoiceId { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public DateTime CreatedAt { get; init; }
}