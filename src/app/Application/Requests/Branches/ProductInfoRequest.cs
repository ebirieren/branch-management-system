namespace Application.Requests;

using Domain.Products;

public record ProductInfoRequest
{
    public int ProductId { get; init; }
    public int RequiredCount { get; init; }
    
}