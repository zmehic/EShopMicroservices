
namespace OrderingApplication.Orders.EventHandlers
{
    public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger) : INotificationHandler<OrderUpdatedEvent>
    {
        public async Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handlet: {DomainEvent}", notification.GetType().Name);
        }
    }
}
