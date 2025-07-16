using Microsoft.Extensions.DependencyInjection;
using OrderTrackingApp.Infrastructure.Messaging;

namespace OrderTrackingApp.Infrastructure.Extensions
{
    public static class InfrastructureServiceRegistrations
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();
            return services;
        }
    }
}
