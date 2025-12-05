namespace OrderTrackingApp.ReadPersistence.Models
{
    public class OrderItemReadModel
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}