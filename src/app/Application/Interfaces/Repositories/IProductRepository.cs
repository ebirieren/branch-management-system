using Domain.Products;

namespace Application.Interfaces;

public interface IProductRepository : IRepository<Product, int>
{
    Task<List<Product>> GetByIdsAsync(IEnumerable<int> productIds);
}