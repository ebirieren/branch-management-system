using Application.DTOs;
using Application.Requests;
using Application.Services;

namespace Application.Interfaces.Services;

public interface IStockTransactionService
{
    Task<StockTransactionDTO?> GetByIdAsync(Guid id);

    Task<IReadOnlyList<StockTransactionDTO?>> GetAllAsync();
}
