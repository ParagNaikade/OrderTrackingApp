using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderTrackingApp.Api.Examples;
using OrderTrackingApp.Api.Models;
using OrderTrackingApp.Application.Orders.Commands;
using OrderTrackingApp.Application.Orders.Queries;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var order = await mediator.Send(new GetOrderByIdQuery(id));
            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] GetOrdersRequest request)
        {
            var query = mapper.Map<GetOrdersQuery>(request);

            var orders = await mediator.Send(query);
            return Ok(orders);
        }
    }
}
