using ECommerce.Persistence.Contacts;
using ECommerce.Persistence.Data;
using ECommerce.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Persistence.Resolver;

//Persistence Layer Dependencies
public static class DependecnyResolverPersistence
{
    public static IServiceCollection Register(this IServiceCollection services,IConfiguration configuration)
    {

        services.AddDbContext<ECommerceDBContext>(options =>
           options.UseNpgsql(configuration.GetConnectionString("ECommercePostgres")));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
