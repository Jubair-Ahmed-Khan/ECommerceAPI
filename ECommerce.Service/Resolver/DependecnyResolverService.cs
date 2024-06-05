using ECommerce.Persistence.Resolver;
using ECommerce.Service.Contacts;
using ECommerce.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Service.Resolver;

//Service Layer Dependencies
public static class DependencyResolverService
{
    public static IServiceCollection Register(this IServiceCollection services,IConfiguration configuration)
    {
        DependecnyResolverPersistence.Register(services,configuration);

        services.AddMemoryCache();
        services.AddScoped<IProductService, ProductService>();

        return services;
        
    }
}
