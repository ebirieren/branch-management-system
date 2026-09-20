using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Requests;
using Domain.Products;

namespace Application.Services;

public class ProductService : IProductService
{

    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDTO?> GetByIdAsync(int id)
    {
        Product? product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return null;
        }

        return new ProductDTO
        {
            Id = product.Id,
            ProductName = product.ProductName,
            ProductPrice = product.ProductPrice,
            PreviousPrice = product.PreviousPrice,
            ProductCount = product.ProductCount,
            TaxRate = product.TaxRate,
            ProductPriceWithTax = product.ProductPriceWithTax,
            BranchId = product.BranchId,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }

    public async Task<IReadOnlyList<ProductDTO>> GetAllAsync()
    {
        List<Product> products = await _productRepository.GetAllAsync();

        return products
            .Select(product => new ProductDTO
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                PreviousPrice = product.PreviousPrice,
                ProductCount = product.ProductCount,
                TaxRate = product.TaxRate,
                ProductPriceWithTax = product.ProductPriceWithTax,
                BranchId = product.BranchId,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            })
            .ToList();
    }

    public async Task<ProductDTO> CreateAsync(CreateProductRequest request)
    {
        Product product = new Product
        {
            ProductName = request.ProductName,
            ProductPrice = request.ProductPrice,
            ProductCount = request.ProductCount,
            TaxRate = request.TaxRate
        };

        await _productRepository.AddAsync(product);

        return new ProductDTO
        {
            Id = product.Id,
            ProductName = product.ProductName,
            ProductPrice = product.ProductPrice,
            PreviousPrice = product.PreviousPrice,
            ProductCount = product.ProductCount,
            TaxRate = product.TaxRate,
            ProductPriceWithTax = product.ProductPriceWithTax,
            BranchId = product.BranchId,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }

    public async Task<ProductDTO?> UpdateAsync(int id, UpdateProductRequest request)
    {
        Product? product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return null;
        }

        product.ProductPrice = request.ProductPrice;
        product.ProductCount = request.ProductCount;
        product.TaxRate = request.TaxRate;
        product.UpdatedAt = DateTime.UtcNow;

        _productRepository.Update(product);

        return new ProductDTO
        {
            Id = product.Id,
            ProductName = product.ProductName,
            ProductPrice = product.ProductPrice,
            PreviousPrice = product.PreviousPrice,
            ProductCount = product.ProductCount,
            TaxRate = product.TaxRate,
            ProductPriceWithTax = product.ProductPriceWithTax,
            BranchId = product.BranchId,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }
    public async Task<bool> DeleteAsync(int id)
    {
        Product? product = await _productRepository.GetByIdAsync(id);

        if(product is null)
        {
            return false;
        }

        _productRepository.Remove(product);

        return true;
    }

}
