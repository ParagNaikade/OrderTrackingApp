using Microsoft.Extensions.DependencyInjection;
using OrderTrackingApp.Application.Commands.Orders;

namespace OrderTrackingApp.Application.Extensions
{
    public static class ApplicationServiceRegistrations
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommandHandler).Assembly));
            return services;
        }
    }
}
