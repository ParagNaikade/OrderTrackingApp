using Microsoft.Extensions.DependencyInjection;
using OrderTrackingApp.Application.MappingProfiles;
using OrderTrackingApp.Application.Orders.Commands;

namespace OrderTrackingApp.Application.Extensions
{
    public static class ApplicationServiceRegistrations
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommandHandler).Assembly));
            services.AddAutoMapper(config => { }, typeof(OrderProfile).Assembly);
            return services;
        }
    }
}
