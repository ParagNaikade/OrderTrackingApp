using FluentValidation;
using OrderTrackingApp.Api.Examples;
using OrderTrackingApp.Api.MappingProfiles;
using OrderTrackingApp.Api.Validators;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json;

namespace OrderTrackingApp.Api.Extensions
{
    public static class ApiServiceRegistrations
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            
            services.AddSwaggerGen(options => {
                options.ExampleFilters();
            });

            services.AddSwaggerExamplesFromAssemblyOf<CreateOrderRequestExample>();

            services.AddControllers()
                        .AddJsonOptions(options =>
                        {
                            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                        });

            services.AddValidatorsFromAssemblyContaining<CreateOrderRequestValidator>();
            services.AddAutoMapper(config => { }, typeof(OrderProfile).Assembly);

            return services;
        }
    }
}
