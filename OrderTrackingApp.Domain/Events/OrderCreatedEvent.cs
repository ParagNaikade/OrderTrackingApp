using MediatR;

namespace OrderTrackingApp.Domain.Events
{
    public class OrderCreatedEvent(Guid orderId) : INotification
    {
        public Guid OrderId { get; } = orderId;
    }
}
