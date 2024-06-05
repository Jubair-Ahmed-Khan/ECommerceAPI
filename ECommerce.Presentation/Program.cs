
using ECommerce.Persistence.Contacts;
using ECommerce.Persistence.Data;
using ECommerce.Persistence.Repositories;
using ECommerce.Service.Contacts;
using ECommerce.Service.Resolver;
using ECommerce.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            DependencyResolverService.Register(builder.Services, builder.Configuration);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
