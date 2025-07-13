using MediatR;
using OrderTrackingApp.Application.DTOs;
using OrderTrackingApp.Application.Interfaces;
using OrderTrackingApp.Domain.Entities;

namespace OrderTrackingApp.Application.Commands.Orders
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
    {
        private readonly IOrderWriteRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderWriteRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                CustomerName = request.CustomerName,
                Status = OrderStatus.Pending,
            };

            await _orderRepository.AddAsync(order);

            return new OrderDto
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                Status = order.Status,
                CreatedAt = order.CreatedAt
            };
        }
    }
}
