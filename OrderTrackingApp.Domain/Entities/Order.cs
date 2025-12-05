namespace OrderTrackingApp.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public Guid CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStatus Status { get; set; }

        public List<OrderItem> Items { get; set; } = [];
    }
}
