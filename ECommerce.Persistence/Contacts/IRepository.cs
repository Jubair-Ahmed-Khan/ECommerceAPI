namespace ECommerce.Persistence.Contacts;

//Generic Repository Interface
public interface IRepository<T> where T : class
{
    Task<IQueryable<T>>GetProducts();
    Task<IQueryable<T>> GetProductBySearchEngineFriendlyName(string searchEngineFriendlyName);
}
