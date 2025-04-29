﻿
namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id,string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record UpdateProductResponse(Product Product);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("products", async (UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);
            })
                .WithTags("Products")
                .WithName("UpdateProduct")
                .WithSummary("Update Product")
                .WithDescription("Update Product")
                .Produces(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
