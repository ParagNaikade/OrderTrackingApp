using OrderTrackingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
