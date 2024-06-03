using ECommerce.Persistence.Contacts;
using ECommerce.Persistence.Model;
using ECommerce.Service.Contacts;
using ECommerce.Service.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetProductBySearchEngineFriendlyName(string name)
        {
            var product = await _productRepository.GetProducts();
            return await product.Where(p => p.SearchEngineFriendlyName.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<List<ResponseDTO>> GetAllProduct(FilterDTO filter, PaginationDTO pagination, string sortBy, bool sortAscending)
        {
            
            var product = await _productRepository.GetProducts();

            
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

            
            // Apply sorting
            switch (sortBy)
            {
                case "Name":
                    product = sortAscending ? product.OrderBy(p => p.Name) : product.OrderByDescending(p => p.Name);
                    break;
                case "SearchEngineFriendlyName":
                    product = sortAscending ? product.OrderBy(p => p.SearchEngineFriendlyName) : product.OrderByDescending(p => p.SearchEngineFriendlyName);
                    break;
                case "CumulativeStock":
                    // Calculate cumulative stock and sort by it
                    product = sortAscending ? product.OrderBy(p => p.Variants.Sum(v => v.Stocks.Sum(s => s.Quantity))) :
                                             product.OrderByDescending(p => p.Variants.Sum(v => v.Stocks.Sum(s => s.Quantity)));
                    break;
                default:
                    product = product.OrderBy(p => p.CreatedOn);
                    break;
            }

            // Apply pagination
            var products = await product.Skip(pagination.PageSize * (pagination.Page - 1))
                                      .Take(pagination.PageSize)
                                      .ToListAsync();

            // Map Product entities to ProductResponseDTO objects
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
}
