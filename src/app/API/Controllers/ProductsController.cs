using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{id:id}")]
    public async Task<ActionResult<CustomerDTO>> GetById(int id)
    {
        ProductDTO? product = await _productService.GetByIdAsync(id);

        if (product is null)
        {
            return NotFound();
        }
        return Ok(product);
    }


    [HttpGet]
    public async Task<ActionResult<List<CustomerDTO>>> GetAll()
    {
        List<ProductDTO?> products = await _productService.GetAllAsync();

        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> Create(CreateProductRequest request)
    {
        ProductDTO product = await _productService.CreateAsync(request);

        return Ok(product);
        
    }
}