using MediatR;
using OrderTrackingApp.Application.Contracts.Orders;

namespace OrderTrackingApp.Application.Orders.Queries
{
    public record GetOrderByIdQuery(Guid OrderId) : IRequest<OrderDto>;
}
