using ECommerce.Persistence.Model;
using ECommerce.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Contacts
{
    public interface IProductService
    {
        Task<Product> GetProductBySearchEngineFriendlyName(string name);
        Task<List<ResponseDTO>> GetAllProduct(FilterDTO filter, PaginationDTO pagination, string sortBy, bool sortAscending);
    }
}
