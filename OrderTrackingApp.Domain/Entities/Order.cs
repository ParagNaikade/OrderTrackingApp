using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTrackingApp.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string CustomerName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public OrderStatus Status { get; set; }
    }
}
