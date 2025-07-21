using OrderTrackingApp.Api.Models;
using Swashbuckle.AspNetCore.Filters;

namespace OrderTrackingApp.Api.Examples;

public class CreateOrderRequestExample : IExamplesProvider<CreateOrderRequest>
{
    public CreateOrderRequest GetExamples()
    {
        return new CreateOrderRequest
        {
            CustomerId = Guid.NewGuid(),
            Items = new List<CreateOrderItemRequest>
            {
                new CreateOrderItemRequest
                {
                    ProductId = Guid.Parse("b111a4dd-f45c-4d40-a6a3-3002f62f823f"),
                    Quantity = 2
                },
                new CreateOrderItemRequest
                {
                    ProductId = Guid.Parse("2a13551b-059c-4035-b275-8a8cb82bd272"),
                    Quantity = 3
                }
            }
        };
    }
}
