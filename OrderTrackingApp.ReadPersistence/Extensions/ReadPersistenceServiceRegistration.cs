using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderTrackingApp.ReadPersistence.Interfaces;
using OrderTrackingApp.ReadPersistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTrackingApp.ReadPersistence.Extensions
{
    public static class ReadPersistenceServiceRegistration
    {
        public static IServiceCollection AddReadPersistence(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<MongoDbContext>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            return services;
        }
    }
}
