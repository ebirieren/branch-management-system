namespace Application.Requests;

public record UpdateProductRequest
{
    public int ProductId { get; init; }
    public decimal ProductPrice { get; init;}
    public int ProductCount { get; init; }
    public int? TaxRate { get; init; }
}