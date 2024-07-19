
using CatalogAPI.Product.CreateProduct;

namespace CatalogAPI.Product.GetProduct
{
   // public record GetProductRequest();
   public record GetProductResponse(IEnumerable <Models.Product>Products);
    public class GetProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
            {
              var result =  await sender.Send(new GetProductQuery());
              var response = result.Adapt<GetProductResponse>();
               return Results.Ok(response);

            }).WithName("GetProduct")
             .Produces<GetProductResponse>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("Get Products")
             .WithDescription("Get Products"); 
        }
    }
}
