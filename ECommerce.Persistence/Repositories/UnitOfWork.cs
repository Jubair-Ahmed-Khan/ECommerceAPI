using ECommerce.Persistence.Contacts;
using ECommerce.Persistence.Data;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ECommerceDBContext _context;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork(ECommerceDBContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories.TryGetValue(typeof(T), out object repository))
            {
                return repository as IRepository<T>;
            }

            var newRepository = new Repository<T>(_context);
            _repositories.Add(typeof(T), newRepository);
            return newRepository;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
