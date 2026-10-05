using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Requests;
using Domain.Products;
using Domain.Branches;

namespace Application.Services;

public class BranchService : IBranchService
{
    private readonly IBranchRepository _branchRepository;
    private readonly IProductRepository _productRepository;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IStockTransactionRepository _stockTransactionRepository;


    public BranchService(
        IBranchRepository branchRepository, 
        IProductRepository productRepository,
        IInvoiceRepository invoiceRepository,
        IStockTransactionRepository stockTransactionRepository)
    {
        _branchRepository = branchRepository;
        _productRepository = productRepository;
        _invoiceRepository = invoiceRepository;
        _stockTransactionRepository = stockTransactionRepository;
    }

    public async Task<BranchDTO?> GetByIdAsync(int id)
    {
        Branch? branch = await _branchRepository.GetByIdAsync(id);

        if (branch is null)
        {
            return null;
        }

        return new BranchDTO
        {
            Id = branch.Id,
            BranchName = branch.BranchName,
            /*Products = branch.Products
                .Select(product => new BranchProductDTO
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    ProductPriceWithTax = product.ProductPriceWithTax ?? 0m,
                    ProductCount = product.ProductCount
                })*/
            Products = branch.BranchStocks
                .Select(stock => new BranchProductDTO
                {
                    Id = stock.ProductId,
                    ProductName = stock.Product.ProductName,
                    ProductPriceWithTax = stock.Product.ProductPriceWithTax ?? 0m,
                    ProductCount = stock.ProductCount
                })
                .ToList(),

            StockTransactions = branch.StockTransactions
                .Select(transaction => new StockTransactionDTO
                {
                    Id = transaction.Id,
                    BranchId = transaction.BranchId,
                    Product = new ProductTransactionDTO
                    {
                        ProductId = transaction.ProductId,
                        ProductName = transaction.ProductNameAtTransaction,
                        SupplierName = transaction.SupplierNameAtTransaction,
                        ProductPrice = transaction.ProductPriceAtTransaction,
                        TaxRate = transaction.TaxRateAtTransaction
                    },
                    Quantity = transaction.Quantity,
                    TransactionDate = transaction.TransactionDate,
                    UnitPriceWithTax = transaction.UnitPriceWithTax,
                    TotalExpense = transaction.TotalExpense,
                    InvoiceId = transaction.InvoiceId,
                    IsInvoiced = transaction.IsInvoiced
                })
                .ToList(),

            InvoiceId = branch.InvoiceId,
            UpdatedAt = branch.UpdatedAt,
            CreatedAt = branch.CreatedAt
        };
    }

    public async Task<IReadOnlyList<BranchDTO?>> GetAllAsync()
    {
        List<Branch> branches = await _branchRepository.GetAllAsync();

        return branches.Select(branch => new BranchDTO
        {
           Id = branch.Id,
           BranchName = branch.BranchName,
           Products = branch.BranchStocks
                .Select(stock => new BranchProductDTO
                {
                    Id = stock.ProductId,
                    ProductName = stock.Product.ProductName,
                    ProductPriceWithTax = stock.Product.ProductPriceWithTax ?? 0m,
                    ProductCount = stock.ProductCount
                })
                .ToList(),

            StockTransactions = branch.StockTransactions
                .Select(transaction => new StockTransactionDTO
                {
                    Id = transaction.Id,
                    BranchId = transaction.BranchId,
                    Product = new ProductTransactionDTO
                    {
                        ProductId = transaction.ProductId,
                        ProductName = transaction.ProductNameAtTransaction,
                        SupplierName = transaction.SupplierNameAtTransaction,
                        ProductPrice = transaction.ProductPriceAtTransaction,
                        TaxRate = transaction.TaxRateAtTransaction
                    },
                    Quantity = transaction.Quantity,
                    TransactionDate = transaction.TransactionDate,
                    UnitPriceWithTax = transaction.UnitPriceWithTax,
                    TotalExpense = transaction.TotalExpense,
                    InvoiceId = transaction.InvoiceId,
                    IsInvoiced = transaction.IsInvoiced
                })
                .ToList(),
                
           InvoiceId = branch.InvoiceId,
           UpdatedAt = branch.UpdatedAt,
           CreatedAt = branch.CreatedAt 
        })
        .ToList();
    }

    public async Task<BranchDTO> CreateAsync(CreateBranchRequest request)
    {
        Branch branch = new Branch
        {
            BranchName = request.BranchName
        };

        await _branchRepository.AddAsync(branch);

        return new BranchDTO
        {
            Id = branch.Id,
            BranchName = branch.BranchName,
            Products = branch.BranchStocks
                .Select(stock => new BranchProductDTO
                {
                    Id = stock.ProductId,
                    ProductName = stock.Product.ProductName,
                    ProductPriceWithTax = stock.Product.ProductPriceWithTax ?? 0m,
                    ProductCount = stock.ProductCount
                })
                .ToList(),
            InvoiceId = branch.InvoiceId,
            UpdatedAt = branch.UpdatedAt,
            CreatedAt = branch.CreatedAt
        };
    }

    public async Task<BranchDTO?> UpdateAsync(UpdateBranchRequest request)
    {
        Branch? branch = await _branchRepository.GetByIdAsync(request.BranchId);

        if (branch is null)
        {
            return null;
        }

        branch.BranchName = request.BranchName;
        branch.UpdatedAt = DateTime.UtcNow;

        _branchRepository.Update(branch);

        return new BranchDTO
        {
          Id = branch.Id,
          BranchName = branch.BranchName,
          Products = branch.BranchStocks
                .Select(stock => new BranchProductDTO
                {
                    Id = stock.ProductId,
                    ProductName = stock.Product.ProductName,
                    ProductPriceWithTax = stock.Product.ProductPriceWithTax ?? 0m,
                    ProductCount = stock.ProductCount
                })
                .ToList(),
          InvoiceId = branch.InvoiceId,
          UpdatedAt = branch.UpdatedAt,
          CreatedAt = branch.CreatedAt  
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        Branch? branch = await _branchRepository.GetByIdAsync(id);

        if(branch is null)
        {
            return false;
        }

        _branchRepository.Remove(branch);

        return true;
    }
}