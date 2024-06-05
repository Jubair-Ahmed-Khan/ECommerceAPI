using ECommerce.Persistence.Contacts;
using ECommerce.Persistence.Model;
using ECommerce.Service.Contacts;
using ECommerce.Service.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ECommerce.Service.Services;

//Product Services
public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMemoryCache _cache;

    public ProductService(IUnitOfWork unitOfWork, IMemoryCache cache)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
    }

    // Get Product by Search Engine Friendly Name
    public async Task<Product> GetProductBySearchEngineFriendlyName(string name)
    {

        var productRepository = _unitOfWork.GetRepository<Product>();
        var products = await productRepository.GetProductBySearchEngineFriendlyName(name);

        string cacheKey = $"cache_{name}";

        if (!_cache.TryGetValue(cacheKey, out Product product))
        {
            if (products == null)
            {
                return null;
            }
            _cache.Set(cacheKey, product);
        }

        return await products.Where(p => p.SearchEngineFriendlyName.ToLower() == name.ToLower()).FirstOrDefaultAsync();
    }

    //Get All Products
    public async Task<List<ResponseDTO>> GetAllProduct(FilterDTO filter, PaginationDTO pagination, string sortBy, bool sortAscending)
    {
        var productRepository = _unitOfWork.GetRepository<Product>();
        var product = await productRepository.GetProducts();

        
        product = product.Include(p => p.Variants)
                     .ThenInclude(v => v.Stocks)
                     .ThenInclude(s => s.Warehouse);


        if (!string.IsNullOrWhiteSpace(filter.ProductName))
        {
            product = product.Where(p => p.Name.ToLower().Contains(filter.ProductName.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(filter.WarehouseName))
        {
            product = product.Where(p => p.Variants.Any(v => v.Stocks.Any(s => s.Warehouse.Name.ToLower() == filter.WarehouseName.ToLower())));
        }

        if (filter.InStock.HasValue)
        {
            if ((bool)filter.InStock)
            {
                product = product.Where(p => p.Variants.Any(v => v.Stocks.Any(s => s.Quantity > 0)));
            }
            else
            {
                product = product.Where(p => !p.Variants.Any(v => v.Stocks.Any(s => s.Quantity > 0)));
            }
        }

        if (!string.IsNullOrWhiteSpace(filter.VariantColor))
        {
            product = product.Where(p => p.Variants.Any(v => v.Color.ToLower() == filter.VariantColor.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(filter.VariantSize))
        {
            product = product.Where(p => p.Variants.Any(v => v.Size.ToLower() == filter.VariantSize.ToLower()));
        }

        
        // Sorting
        switch (sortBy)
        {
            case "Name":
                // Sorting by Name
                product = sortAscending ? product.OrderBy(p => p.Name) : product.OrderByDescending(p => p.Name);
                break;
            case "SearchEngineFriendlyName":
                // Sorting by SearchEngineFriendlyName
                product = sortAscending ? product.OrderBy(p => p.SearchEngineFriendlyName) : product.OrderByDescending(p => p.SearchEngineFriendlyName);
                break;
            case "CumulativeStock":
                // Cumulative Stock Sorting
                product = sortAscending ? product.OrderBy(p => p.Variants.Sum(v => v.Stocks.Sum(s => s.Quantity))) :
                                         product.OrderByDescending(p => p.Variants.Sum(v => v.Stocks.Sum(s => s.Quantity)));
                break;
            default:
                product = product.OrderBy(p => p.CreatedOn);
                break;
        }

        //Pagination
        var products = await product.Skip(pagination.PageSize * (pagination.Page - 1))
                                  .Take(pagination.PageSize)
                                  .ToListAsync();

        //Product to ProductResponseDTO Mapping
        var productDtos = products.Select(product => new ResponseDTO
        {
            ProductName = product.Name,
            InStock = product.Variants.Any(v => v.Stocks.Any(s => s.Quantity > 0)),
            Variants = product.Variants.Select(v => new VariantDTO
            {
                Color = v.Color,
                Size = v.Size,
                WarehouseStocks = v.Stocks.Select(s => new WarehouseStockDTO
                {
                    WarehouseName = s.Warehouse.Name,
                    Quantity = s.Quantity
                }).ToList()
            }).ToList()
        }).ToList();

        return productDtos;
    }
}
