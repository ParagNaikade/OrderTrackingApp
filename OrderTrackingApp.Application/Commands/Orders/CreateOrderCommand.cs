using MediatR;
using OrderTrackingApp.Application.DTOs;

namespace OrderTrackingApp.Application.Commands.Orders
{
    public class CreateOrderCommand : IRequest<OrderDto>
    {
        public string CustomerName { get; set; } = string.Empty;
    }
}
