using Microsoft.Extensions.DependencyInjection;
using OrderTrackingApp.Infrastructure.EventHandlers;
using OrderTrackingApp.Infrastructure.Messaging;

namespace OrderTrackingApp.Infrastructure.Extensions
{
    public static class InfrastructureServiceRegistrations
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(OrderCreatedEventHandler).Assembly));

            return services;
        }
    }
}
