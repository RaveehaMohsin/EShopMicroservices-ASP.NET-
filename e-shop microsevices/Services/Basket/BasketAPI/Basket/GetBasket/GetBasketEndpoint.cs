﻿namespace BasketAPI.Basket.GetBasket
{
    //public record GetBasketRequest(string Username);
    public record GetBasketResponse(ShoppingCart Cart);
    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName , ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(userName));
                var response = result.Adapt<GetBasketResponse>();

                return Results.Ok(response);
            }).WithName("GetProductById")
             .Produces<GetBasketResponse>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("Get Product by Id")
             .WithDescription("Get Product by Id");
        }
    }
}
