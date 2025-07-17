using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderTrackingApp.Api.Models;
using OrderTrackingApp.Application.Commands.Orders;

namespace OrderTrackingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var command = mapper.Map<CreateOrderCommand>(request);
            var orderId = await mediator.Send(command);

            return CreatedAtAction(nameof(CreateOrder), new { id = orderId }, new { OrderId = orderId });
        }
    }
}
