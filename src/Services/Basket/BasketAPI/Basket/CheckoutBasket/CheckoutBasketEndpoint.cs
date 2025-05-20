﻿using BasketAPI.DTOs;

namespace BasketAPI.Basket.CheckoutBasket
{
    public record CheckoutBasketRequest(BasketCheckoutDTO BasketCheckoutDTO);
    public record CheckoutBasketResponse(bool IsSuccess);
    public class CheckoutBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/checkout", async (CheckoutBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<CheckoutBasketCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CheckoutBasketResponse>();
                return Results.Ok(response);
            })
                .WithName("CheckoutBasket")
                .Produces<CheckoutBasketResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Checkout Basket")
                .WithDescription("Checkout Basket Description");
        }
    }
}
