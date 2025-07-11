using MediatR;
using OrderTrackingApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTrackingApp.Application.Commands.Orders
{
    public class CreateOrderCommand : IRequest<OrderDto>
    {
        public string CustomerName { get; set; } = string.Empty;
    }
}
