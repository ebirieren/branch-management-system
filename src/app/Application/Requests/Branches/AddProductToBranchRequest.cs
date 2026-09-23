namespace Application.Requests;
using Domain.Products;
using Application.Requests;
public record AddProductToBranchRequest
{
    public int BranchId { get; init; }
    public ICollection<ProductInfoRequest> ProductInfo { get; init; }
}