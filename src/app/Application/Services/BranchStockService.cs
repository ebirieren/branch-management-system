using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Requests;
using Domain.Branches;
using Domain.Products;

namespace Application.Services;

public class BranchStockService : IBranchStockService
{
    private readonly IBranchRepository _branchRepository;
    private readonly IProductRepository _productRepository;

    public BranchStockService(IBranchRepository branchRepository, IProductRepository productRepository)
    {
        _branchRepository = branchRepository;
        _productRepository = productRepository;
    }


    public async Task<BranchDTO?> AddProductAsync(AddProductToBranchRequest request)
    {
        Branch? branch = await _branchRepository.GetByIdAsync(request.BranchId);
        
        if(branch is null)
        {
            return null;
        }

        var requestedItems = request.ProductInfo
            .GroupBy(item => item.ProductId)
            .Select(group => new ProductInfoRequest
            {
                ProductId = group.Key,
                RequiredCount = group.Sum(item => item.RequiredCount)
            })
            .ToList();

        int[] productIds = requestedItems
            .Select(item => item.ProductId)
            .ToArray();

        List<Product> products = await _productRepository.GetByIdsAsync(productIds);

        Dictionary<int, Product> productsById = products.ToDictionary(product => product.Id);

        foreach (ProductInfoRequest item in requestedItems)
        {
            //Console.WriteLine(product);    

            if (!productsById.TryGetValue(item.ProductId, out Product? product))
            {
                //return "Asked Product couldn't found!";
                continue;
            }

            if (product.ProductCount < item.RequiredCount || item.RequiredCount <= 0)
            {
                //return "There is no enough amount that product"!;
                continue;
            }

            BranchStock? branchStock = branch.BranchStocks
                .FirstOrDefault(stock => stock.ProductId == product.Id);

            if (branchStock is null)
            {
                branchStock = new BranchStock
                {
                    BranchId = branch.Id,
                    ProductId = product.Id,
                    Product = product,
                    ProductCount = item.RequiredCount
                };

                branch.BranchStocks.Add(branchStock);
            }
            else
            {
                branchStock.ProductCount += item.RequiredCount;
            }

            product.ProductCount -= item.RequiredCount;
        }

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
}