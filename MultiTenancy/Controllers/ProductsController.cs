using Microsoft.AspNetCore.Mvc;
using MultiTenancy.Dtos;

namespace MultiTenancy.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    public string TenantId { get; set; }
    private readonly ITenantService _tenantService;
    public ProductsController(IProductService productService, ITenantService tenantService)
    {
        _tenantService = tenantService;
        _productService = productService;
        TenantId = _tenantService.GetCurrentTenant()?.TId;
    }

    [HttpGet]
    public async Task<ActionResult<ProductDto>> GetAllAsync()
    {
        return Ok(new ProductDto
        {
            Items = await _productService.GetAllAsync(),
            totalItem = await _productService.GetCountAsync()
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> CreatedAsync(CreateProductDto dto)
    {
        Product product = new ()
        {
            Name = dto.Name,
            Description = dto.Description,
            Rate = dto.Rate,
        };

        var createdProduct = await _productService.CreatedAsync(product);

        return Ok(createdProduct);
    }
}