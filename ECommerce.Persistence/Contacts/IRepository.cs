using ECommerce.Persistence.Model;

namespace ECommerce.Persistence.Contacts
{
    public interface IRepository<T> where T : class
    {
        Task<IQueryable<T>>GetProducts();
        Task<IQueryable<T>> GetProductBySearchEngineFriendlyName(string searchEngineFriendlyName);
    }
}
