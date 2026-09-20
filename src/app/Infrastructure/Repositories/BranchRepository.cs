namespace Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Domain.Branches;
using Application.Interfaces;

public class BranchRepository : IBranchRepository
{
    private readonly AppDBContext _context;

    public BranchRepository(AppDBContext context)
    {
        _context = context;
    }

    public Task<Branch?> GetByIdAsync(int id)
    {
        return _context.Branches.FirstOrDefaultAsync(branch => branch.Id == id);
    }

    public Task<List<Branch>> GetAllAsync()
    {
        return _context.Branches.ToListAsync();
    }

    public async Task AddAsync(Branch branch)
    {
        await _context.Branches.AddAsync(branch);
        await _context.SaveChangesAsync();
    }

    public void Update(Branch branch)
    {
        _context.Branches.Update(branch);
        _context.SaveChanges();
    }

    public void Remove(Branch branch)
    {
        _context.Branches.Remove(branch);
        _context.SaveChanges();
    }
}
