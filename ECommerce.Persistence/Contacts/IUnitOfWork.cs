namespace ECommerce.Persistence.Contacts;

//Unit of Work Interface
public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : class;
    int SaveChanges();
}
