namespace Infrastructure.Repositories;

public class ProductRepository: Repository<Product>, IRepository
{
    private readonly AppDBContext _context;

    public ProductRepository(AppDBContext context) : base(context)
    {
        _context = context;
    }

    public Task<List<Product?>> GetByIdAsync(int id)
    {
        return _context.Products
            .Where(products => products.Id == id)
            .ToListAsync();
    }
    
    public Task<List<Product?>> GetAllAsync()
    {
        return _context.Products.ToListAsync();
    }

    public Task AddAsync(Product product)
    {
        return _context.Products
            .AddAsync(product);
    }

    public void Update(Product product)
    {
        return _context.Products
            .Update(product);
    }

    public void Remove(Product product)
    {
        return _context.Products
            .Remove(product);
    }

    
}