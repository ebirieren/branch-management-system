namespace Application.Requests;

public record UpdateProductRecord
{
    public int Id { get; init; }
    public decimal ProductPrice { get; init;}
    public int ProductCount { get; init; }
    public int? TaxRate { get; init; }
}