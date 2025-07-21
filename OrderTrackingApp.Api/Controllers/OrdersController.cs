using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderTrackingApp.Api.Examples;
using OrderTrackingApp.Api.Models;
using OrderTrackingApp.Application.Commands.Orders;
using Swashbuckle.AspNetCore.Filters;

namespace OrderTrackingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpPost]

        [SwaggerRequestExample(typeof(CreateOrderRequest), typeof(CreateOrderRequestExample))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var command = mapper.Map<CreateOrderCommand>(request);
            var orderId = await mediator.Send(command);

            return CreatedAtAction(nameof(CreateOrder), new { id = orderId }, new { OrderId = orderId });
        }
    }
}
