namespace Application.DTOs;

public record ProductDTO
{
    public int Id { get; init; }
    public string ProductName { get; init;} = string.Empty;
    public decimal ProductPrice { get; init; }
    public decimal? PreviousPrice { get; init; }
    public int ProductCount { get; init; }
    public int? TaxRate { get; init; }
    public decimal? ProductPriceWithTax { get; init; }
    public Guid BranchId { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
