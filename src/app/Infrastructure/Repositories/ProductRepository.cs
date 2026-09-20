namespace Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Domain.Products;
using Application.Interfaces;

public class ProductRepository : IProductRepository
{
    private readonly AppDBContext _context;

    public ProductRepository(AppDBContext context)
    {
        _context = context;
    }

    public Task<Product?> GetByIdAsync(int id)
    {
        return _context.Products
            .FirstOrDefaultAsync(product => product.Id == id);
    }
    
    public Task<List<Product>> GetAllAsync()
    {
        return _context.Products.ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
    }

    public void Remove(Product product)
    {
        _context.Products.Remove(product);
        _context.SaveChanges();
    }

    
}
