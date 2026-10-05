namespace Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Domain.Transactions;
using Application.Interfaces;

public class StockTransactionRepository : IStockTransactionRepository
{
    private readonly AppDBContext _context;

    public StockTransactionRepository(AppDBContext context)
    {
        _context = context;
    }

    public Task<StockTransaction?> GetByIdAsync(Guid id)
    {
        return _context.StockTransactions
            .FirstOrDefaultAsync(transaction => transaction.Id == id);
            
    }

    public Task<List<StockTransaction>> GetAllAsync()
    {
        return _context.StockTransactions
        .ToListAsync();
    }

    public async Task AddAsync(StockTransaction transaction)
    {
        await _context.StockTransactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
    }

    public void Update(StockTransaction transaction)
    {
        _context.StockTransactions.Update(transaction);
        _context.SaveChanges();
    }

    public void Remove(StockTransaction transaction)
    {
        _context.StockTransactions.Remove(transaction);
        _context.SaveChanges();
    }
}
