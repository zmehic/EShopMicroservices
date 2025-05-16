namespace OrderingApplication.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(OrderDTO Order) : ICommand<UpdateOrderResult>;

    public record UpdateOrderResult(bool IsSuccess);

    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("CustomerId is required");
            RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
        }
    }
}
