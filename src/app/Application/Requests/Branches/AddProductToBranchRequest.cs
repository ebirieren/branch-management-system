namespace Application.Requests;
using Domain.Products;
public record AddProductToBranchRequest
{
    public int BranchId { get; init; }
    public ICollection<int> ProductId { get; init; } = new List<int>();
    //public required ICollection<Product> Products { get; init; } = new List<Product>();
}