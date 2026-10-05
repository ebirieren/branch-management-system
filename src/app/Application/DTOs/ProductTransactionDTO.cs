namespace Application.DTOs;
using Application.DTOs;

public record ProductTransactionDTO
{
    public int ProductId { get; init; }
    public string ProductName { get; init; } = string.Empty;
    public string SupplierName { get; init; } = string.Empty;
    public decimal ProductPrice { get; init; }
    public int? TaxRate { get; init; }
}
