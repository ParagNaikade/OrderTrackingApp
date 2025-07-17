using OrderTrackingApp.Domain.Entities;

namespace OrderTrackingApp.ReadPersistence.Models
{
    public class OrderReadModel
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<OrderItemReadModel> Items { get; set; } = [];
    }
}