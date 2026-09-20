using Domain.Products;

namespace Application.Interfaces;

public interface IProductRepository : IRepository<Product, int>
{
}