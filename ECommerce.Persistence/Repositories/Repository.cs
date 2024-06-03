using ECommerce.Persistence.Contacts;
using ECommerce.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ECommerceDBContext _context;
        public Repository(ECommerceDBContext context)
        {
            _context = context;
        }
        public async Task<IQueryable<T>> GetProducts()
        {
            return _context.Set<T>().AsQueryable();
        }
    }
}
