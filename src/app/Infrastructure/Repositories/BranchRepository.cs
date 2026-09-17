namespace Infrastructure.Repositories;

public class BranchRepository: Repository<Branch>, IRepository
{
    private readonly AppDBContext _context;

    public BranchRepository(AppDBContext context) : base(context)
    {
        _context = context;
    }

    
}