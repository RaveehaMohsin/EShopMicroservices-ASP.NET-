
using CatalogAPI.Product.GetProduct;

namespace CatalogAPI.Product.GetProductbyId
{
    //public record GetProductByIdRequest();

    public record GetProductByIdResponse(Models.Product Product);
    public class GetProductByIdEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id));
                var response = result.Adapt<GetProductByIdResponse>();
                return Results.Ok(response);
            }).WithName("GetProductById")
             .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("Get Product by Id")
             .WithDescription("Get Product by Id");
        }
    }
}
