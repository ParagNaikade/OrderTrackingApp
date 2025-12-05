using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderTrackingApp.Application.Contracts.Orders;
using OrderTrackingApp.Domain.Interfaces;
using OrderTrackingApp.Persistence.Repositories;

namespace OrderTrackingApp.Persistence.Extensions
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("SqlConnection")));

            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
