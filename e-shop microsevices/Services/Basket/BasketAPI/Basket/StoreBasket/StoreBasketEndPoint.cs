
namespace BasketAPI.Basket.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart Cart);
    public record StoreBasketResponse(string Username);
    public class StoreBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request ,ISender sender) =>
            {
               var command =  request.Adapt<StoreBaskeCommand>();
               var result = await sender.Send(command);
               var response = result.Adapt<StoreBasketResponse>();

               return Results.Created($"/basket/{response.Username}", response);
            }).
              WithName("CreateBasket")
             .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("CreateBasket")
             .WithDescription("CreateBasket");
        }
    }
}
