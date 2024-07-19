﻿
using CatalogAPI.Product.GetProduct;

namespace CatalogAPI.Product.DeleteProduct
{
    // public record DeleteProductRequest(Guid id);

    public record DeleteProductResponse(bool IsSuccess);

    public class DeleteProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}" , async(Guid id , ISender sender)=>
            {
                var result = await sender.Send(new DeleteProductCommand(id));
                var response = result.Adapt<DeleteProductResponse>();

                return Results.Ok(response);

            }).WithName("Delete Product")
             .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("Delete Product")
             .WithDescription("Delete Product");
        }
    }
}
