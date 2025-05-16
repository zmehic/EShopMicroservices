﻿namespace OrderingApplication.Orders.Commands.DeleteOrder
{
    public class DeleteOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.orderId);
            var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);
        
            if (order == null)
            {
                throw new OrderNotFoundException(command.orderId);
            }

            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteOrderResult(true);
        }
    }
}
