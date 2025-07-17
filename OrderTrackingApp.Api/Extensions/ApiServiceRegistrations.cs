using FluentValidation;
using OrderTrackingApp.Api.MappingProfiles;
using OrderTrackingApp.Api.Validators;
using System.Text.Json;

namespace OrderTrackingApp.Api.Extensions
{
    public static class ApiServiceRegistrations
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddControllers()
                        .AddJsonOptions(options =>
                        {
                            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                        });

            services.AddValidatorsFromAssemblyContaining<CreateOrderRequestValidator>();
            services.AddAutoMapper(config => { }, typeof(OrderMappingProfile).Assembly);

            return services;
        }
    }
}
