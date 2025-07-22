using Microsoft.Extensions.DependencyInjection;
using OrderTrackingApp.Application.Contracts.Orders;
using OrderTrackingApp.ReadPersistence.Repositories;

namespace OrderTrackingApp.ReadPersistence.Extensions
{
    public static class ReadPersistenceServiceRegistration
    {
        public static IServiceCollection AddReadPersistence(this IServiceCollection services)
        {
            services.AddSingleton<MongoDbContext>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();

            services.AddAutoMapper(config => { }, typeof(MappingProfiles.OrderProfile).Assembly);
            return services;
        }
    }
}
