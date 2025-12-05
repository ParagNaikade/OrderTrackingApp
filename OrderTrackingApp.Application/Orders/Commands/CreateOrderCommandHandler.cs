using MediatR;
using OrderTrackingApp.Application.Contracts.Orders;
using OrderTrackingApp.Domain.Entities;
using OrderTrackingApp.Domain.Events;
using OrderTrackingApp.Domain.Interfaces;

namespace OrderTrackingApp.Application.Orders.Commands
{
    public class CreateOrderCommandHandler(IOrderWriteRepository orderRepository, IProductRepository productRepository, IMediator mediator)
        : IRequestHandler<CreateOrderCommand, Guid>
    {
        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderItems = new List<OrderItemDto>();
            var updatedProducts = new List<Product>();

            var productIds = request.Items.Select(item => item.ProductId).ToList();
            var products = await productRepository.GetByIdsAsync(productIds);
            var productDict = products.ToDictionary(p => p.Id, p => p);

            foreach (var item in request.Items)
            {
                if (!productDict.TryGetValue(item.ProductId, out Product? product))
                {
                    throw new KeyNotFoundException($"Product with ID {item.ProductId} not found.");
                }

                if(product.StockQuantity < item.Quantity)
                {
                    throw new InvalidOperationException($"Insufficient stock for product {product.Name}. Available: {product.StockQuantity}, Requested: {item.Quantity}.");
                }

                orderItems.Add(new OrderItemDto { ProductId = item.ProductId, Quantity = item.Quantity, UnitPrice = product.Price });

                product.StockQuantity -= item.Quantity;

                updatedProducts.Add(product);
            }

            await productRepository.UpdateProducts(updatedProducts);

            var order = new OrderDto
            {
                Id = Guid.NewGuid(),
                OrderNumber = $"ORD-{DateTime.UtcNow.Ticks}",
                CustomerId = request.CustomerId,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                Items = orderItems
            };

            await orderRepository.AddAsync(order);

            await mediator.Publish(new OrderCreatedEvent(order.Id), cancellationToken);

            return order.Id;
        }
    }
}
