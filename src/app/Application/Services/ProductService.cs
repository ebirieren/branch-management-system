using Application.DTOs;
using Application.Interfaces;
using Application.Requests;

namespace Application.Services;

public class ProductService
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

        return new ProductDTO(
            product.Id,
            product.ProductName,
            product.ProductPrice,
            product.PreviousPrice,
            product.ProductCount,
            product.TaxRate,
            product.ProductPriceWithTax,
            product.BranchId,
            product.CreatedAt,
            product.UpdatedAt
        );
    }
     public async Task<IReadOnlyList<ProductDTO?>> GetAllAsync()
    {
        List<Product?> products = await _productRepository.GetAllAsync();

        if (products is null)
        {
            return null;
        }

        return products
            .Select(product => new ProductDTO(
                product.Id,
                product.ProductName,
                product.ProductPrice,
                product.PreviousPrice,
                product.ProductCount,
                product.TaxRate,
                product.ProductPriceWithTax,
                product.BranchId,
                product.CreatedAt,
                product.UpdatedAt
            ))
            .ToListAsync();
    }

    public async Task<ProductDTO> CreateAsync(CreateProductRequest request)
    {
        Product product = new Product(
            RowGuid = Guid.NewGuid,
            Id = null,
            ProductName = request.ProductName,
            ProductPrice = request.ProductPrice,
            PreviousPrice = null,
            ProductCount = request.ProductCount,
            TaxRate = request.TaxRate,
            ProductPriceWithTax = null,
            BranchId = null,
            Branches = null,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        );

        await _productRepository.CreateAsync(product);

        return new ProductDTO(
            product.Id,
            product.ProductName,
            product.ProductPrice,
            product.PreviousPrice,
            product.ProductCount,
            product.TaxRate,
            product.ProductPriceWithTax,
            product.BranchId,
            product.CreateAt,
            product.UpdatedAt
        );
    }

    public async Task<ProductDTO> UpdateAsync(int id, UpdateProductRequest request)
    {
        Product product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return null;
        }

        product.ProductPrice = request.ProductPrice;
        product.ProductCount = request.ProductCount;
        product.TaxRate = request.TaxRate;
        product.UpdatedAt = Datetime.UtcNow;

        await _productRepository.Update(product);

        return new ProductDTO(
            product.Id,
            product.ProductName,
            product.ProductPrice,
            product.PreviousPrice,
            product.ProductCount,
            product.TaxRate,
            product.ProductPriceWithTax,
            product.BranchId,
            product.CreateAt,
            product.UpdatedAt
        );
    }
    public async Task<bool> DeleteAsync(int id)
    {
        Product product = await _productRepository.GetByIdAsync(id);

        if(product is null)
        {
            return false;
        }

        await _productRepository.Remove(product);

        return true;
    }

}