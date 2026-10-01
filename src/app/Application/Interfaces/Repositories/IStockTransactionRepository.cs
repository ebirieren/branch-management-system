using Domain.Transactions;

namespace Application.Interfaces;

public interface ITransactionRepository : IRepository<StockTransaction, Guid>
{
    
}