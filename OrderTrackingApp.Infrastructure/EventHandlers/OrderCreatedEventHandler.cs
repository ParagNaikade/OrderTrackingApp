using MediatR;
using OrderTrackingApp.Domain.Events;
using OrderTrackingApp.Infrastructure.Messaging;

namespace OrderTrackingApp.Infrastructure.EventHandlers
{
    internal class OrderCreatedEventHandler(IMessagePublisher messagePublisher) : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            await messagePublisher.PublishAsync("orders", notification);
        }
    }
}
