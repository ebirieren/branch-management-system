using Application.DTOs;

namespace Application.Interfaces.Services;

public interface IProductService
{
    Task<ProductDTO?> GetByIdAsync(int id);

    Task<IReadOnlyList<ProductDTO?>> GetAllAsync();

    Task<ProductDTO> CreateAsync(CreateProductRequest request);

    Task<ProductDTO> UpdateAsync(int id, UpdateProductRequest request);

    Task<bool> DeleteAsync(int id);

    //Assign a branch
    //remove from branch
}