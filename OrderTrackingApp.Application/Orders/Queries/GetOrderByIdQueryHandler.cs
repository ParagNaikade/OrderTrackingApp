using MediatR;
using OrderTrackingApp.Application.Contracts.Orders;

namespace OrderTrackingApp.Application.Orders.Queries
{
    public class GetOrderByIdQueryHandler(IOrderReadRepository orderReadRepository) : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await orderReadRepository.GetByIdAsync(request.OrderId);
            return order ?? throw new Exception($"Order not found for ID {request.OrderId}");
        }
    }
}
