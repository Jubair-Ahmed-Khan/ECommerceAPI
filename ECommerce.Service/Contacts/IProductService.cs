using ECommerce.Persistence.Model;
using ECommerce.Service.DTOs;
namespace ECommerce.Service.Contacts;

//Product Service Interface
public interface IProductService
{
    Task<Product> GetProductBySearchEngineFriendlyName(string name);
    Task<List<ResponseDTO>> GetAllProduct(FilterDTO filter, PaginationDTO pagination, string sortBy, bool sortAscending);
}
