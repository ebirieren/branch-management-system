namespace Application.Requests;

public record UpdateProductRequest
{
    public decimal ProductPrice { get; init;}
    public int ProductCount { get; init; }
    public int? TaxRate { get; init; }
}