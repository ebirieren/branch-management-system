using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Requests;
using Application.Services;
using Domain.Transactions;

namespace Application.Services;

public class StockTransactionService : IStockTransactionService
{
    private readonly IStockTransactionRepository _stockTransactionRepository;

    public StockTransactionService(IStockTransactionRepository stockTransactionRepository)
    {
        _stockTransactionRepository = stockTransactionRepository;
    }
    public async Task<StockTransactionDTO?> GetByIdAsync(Guid id)
    {
        StockTransaction? stockTransaction = await _stockTransactionRepository.GetByIdAsync(id);

        return new StockTransactionDTO
        {
            Id = stockTransaction.Id,
            BranchId = stockTransaction.BranchId,
            
            Product = new ProductTransactionDTO
            {
                ProductId = stockTransaction.ProductId,
                ProductName = stockTransaction.ProductNameAtTransaction,
                SupplierName = stockTransaction.SupplierNameAtTransaction,
                ProductPrice = stockTransaction.ProductPriceAtTransaction,
                TaxRate = stockTransaction.TaxRateAtTransaction ?? 0
            },

            Quantity = stockTransaction.Quantity,
            TransactionDate = stockTransaction.TransactionDate,
            UnitPriceWithTax = stockTransaction.UnitPriceWithTax,
            TotalExpense = stockTransaction.TotalExpense,
            InvoiceId = stockTransaction.InvoiceId,
            IsInvoiced = stockTransaction.IsInvoiced
        };
    }

    public async Task<IReadOnlyList<StockTransactionDTO?>> GetAllAsync()
    {
        List<StockTransaction?> transaction = await _stockTransactionRepository.GetAllAsync();

        return transaction.Select(stockTransaction => new StockTransactionDTO
        {
            Id = stockTransaction.Id,
            BranchId = stockTransaction.BranchId,
            
            Product = new ProductTransactionDTO
            {
                ProductId = stockTransaction.ProductId,
                ProductName = stockTransaction.ProductNameAtTransaction,
                SupplierName = stockTransaction.SupplierNameAtTransaction,
                ProductPrice = stockTransaction.ProductPriceAtTransaction,
                TaxRate = stockTransaction.TaxRateAtTransaction ?? 0
            },

            Quantity = stockTransaction.Quantity,
            TransactionDate = stockTransaction.TransactionDate,
            UnitPriceWithTax = stockTransaction.UnitPriceWithTax,
            TotalExpense = stockTransaction.TotalExpense,
            InvoiceId = stockTransaction.InvoiceId,
            IsInvoiced = stockTransaction.IsInvoiced
        })
        .ToList();
    }
}
