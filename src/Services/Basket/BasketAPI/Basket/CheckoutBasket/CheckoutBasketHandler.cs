using BasketAPI.Data;
using BasketAPI.DTOs;
using BuildingBlocksMessaging.Events;
using MassTransit;

namespace BasketAPI.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDTO BasketCheckoutDto) : ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool IsSuccess);

    public class CheckoutBasketCommandValidator
        : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDTO must not be null");
            RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithErrorCode("UserName must not be empty");
        }
    }
    public class CheckoutBasketHandler
        (IBasketRepository repository, IPublishEndpoint publishEndpoint)
        : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken);
            if(basket == null)
            {
                return new CheckoutBasketResult(false);
            }

            var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;

            await publishEndpoint.Publish(eventMessage,cancellationToken);
            await repository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);

            return new CheckoutBasketResult(true);
        }
    }
}
