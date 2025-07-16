using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderTrackingApp.Infrastructure.Messaging;

namespace OrderTrackingApp.Infrastructure.Extensions
{
    public static class InfrastructureServiceRegistrations
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();
            return services;
        }
    }
}
