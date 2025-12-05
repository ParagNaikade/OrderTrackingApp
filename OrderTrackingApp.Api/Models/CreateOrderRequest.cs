using System.ComponentModel.DataAnnotations;

namespace OrderTrackingApp.Api.Models
{
    public class CreateOrderRequest
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public List<CreateOrderItemRequest> Items { get; set; } = [];
    }

    public class CreateOrderItemRequest
    {
        [Required]
        public Guid ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
