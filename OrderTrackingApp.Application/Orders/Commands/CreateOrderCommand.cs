using MediatR;

namespace OrderTrackingApp.Application.Orders.Commands
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid CustomerId { get; set; }

        public List<CreateOrderItemDto> Items { get; set; } = [];
    }

    public class CreateOrderItemDto
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
