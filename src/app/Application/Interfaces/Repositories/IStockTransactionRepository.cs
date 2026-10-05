using Domain.Transactions;

namespace Application.Interfaces;

public interface IStockTransactionRepository : IRepository<StockTransaction, Guid>
{
    
}