using Application.DTOs;
using Application.Requests;
using Application.Services;

namespace Application.Interfaces.Services;

public interface IProductService : IService<ProductDTO, int, CreateProductRequest, UpdateProductRequest>
{
    //Assign a branch
    //remove from branch
}
