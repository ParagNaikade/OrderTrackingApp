using OrderTrackingApp.Domain.Entities;

namespace OrderTrackingApp.Application.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public OrderStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
