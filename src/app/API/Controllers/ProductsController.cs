using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Interfaces.Services;
using Application.Requests;

namespace API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDTO>> GetById(int id)
    {
        ProductDTO? product = await _productService.GetByIdAsync(id);

        if (product is null)
        {
            return NotFound();
        }
        return Ok(product);
    }


    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductDTO>>> GetAll()
    {
        IReadOnlyList<ProductDTO> products = await _productService.GetAllAsync();

        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> Create(CreateProductRequest request)
    {
        ProductDTO product = await _productService.CreateAsync(request);

        return Ok(product);
        
    }
}
