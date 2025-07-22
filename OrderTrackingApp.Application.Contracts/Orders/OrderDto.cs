using OrderTrackingApp.Domain.Entities;

namespace OrderTrackingApp.Application.Contracts.Orders
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public OrderStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid CustomerId { get; set; }

        public List<OrderItemDto> Items { get; set; } = [];
    }

    public class OrderItemDto
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
