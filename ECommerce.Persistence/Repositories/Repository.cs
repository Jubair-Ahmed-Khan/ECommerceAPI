using ECommerce.Persistence.Contacts;
using ECommerce.Persistence.Data;
using ECommerce.Persistence.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        public Repository(ECommerceDBContext context)
        {
            _dbSet = context.Set<T>();
        }
        public async Task<IQueryable<T>> GetProducts()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<IQueryable<T>> GetProductBySearchEngineFriendlyName(string searchEngineFriendlyName)
        {
            return _dbSet.AsQueryable();
        }
    }
}
