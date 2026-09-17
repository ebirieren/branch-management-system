namespace Infrastructure.Repositories;

public class ProductRepository: Repository<Product>, IRepository
{
    private readonly AppDBContext _context;

    public ProductRepository(AppDBContext context) : base(context)
    {
        _context = context;
    }

    
}