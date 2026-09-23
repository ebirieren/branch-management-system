using Domain.Products;
using Domain.Branches;

namespace Application.DTOs;
public record BranchProductDTO
{
    public int Id { get; init; }
    public string ProductName { get; init; } = string.Empty;
    public decimal? ProductPriceWithTax { get; init; }
    public int ProductCount { get; init; }

    
}