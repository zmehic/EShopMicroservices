﻿
namespace CatalogAPI.Products.GetProductsByCategory
{
    public record GetProductsByCategoryResponse(IEnumerable<Product> Products);
    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var query = new GetProductByCategoryQuery(category);
                var result = await sender.Send(query);
                var response = result.Adapt<GetProductsByCategoryResponse>();

                return Results.Ok(response);
            }).WithTags("Products")
            .WithName("GetProductsByCategory")
            .WithSummary("Get Products By Category")
            .WithDescription("Get Products By Category")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
