using ECommerce.Persistence.Model;
using ECommerce.Service.Contacts;
using ECommerce.Service.DTOs;
using Microsoft.AspNetCore.Mvc;
namespace ECommerce.Presentation.Controllers;

//Product Controller
[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("GetAllProducts")]
    public async Task<ActionResult<IEnumerable<ResponseDTO>>> GetProducts(
        [FromQuery] string? productName,
        [FromQuery] string? warehouseName,
        [FromQuery] string? variantColor,
        [FromQuery] string? variantSize,
        [FromQuery] bool? inStock,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string sortBy = "Name",
        [FromQuery] bool sortAscending = false)
    {
        var filter = new FilterDTO
        {
            ProductName = productName,
            WarehouseName = warehouseName,
            VariantColor = variantColor,
            VariantSize = variantSize,
            InStock = inStock,
        };

        var pagination = new PaginationDTO
        {
            Page = page,
            PageSize = pageSize
        };

        var products = await _productService.GetAllProduct(filter, pagination, sortBy, sortAscending);

        return Ok(products);
    }

    [HttpGet("{searchEngineFriendlyName}")]
    public async Task<ActionResult<Product>> GetProductBySearchEngineFriendlyName(string searchEngineFriendlyName)
    {
        var product = await _productService.GetProductBySearchEngineFriendlyName(searchEngineFriendlyName);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }
}
