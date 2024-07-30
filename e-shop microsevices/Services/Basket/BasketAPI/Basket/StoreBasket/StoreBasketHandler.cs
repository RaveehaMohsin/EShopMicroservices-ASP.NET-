using DiscountGrpc;

namespace BasketAPI.Basket.StoreBasket
{
    public record StoreBaskeCommand(ShoppingCart Cart): ICommand<StoreBasketResult>;
    public record StoreBasketResult(string Username);

    public class StoreBasketHandlerValidator : AbstractValidator<StoreBaskeCommand>
    {
        public StoreBasketHandlerValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart can't be null");
            RuleFor(x => x.Cart.Username).NotEmpty().WithMessage("Username is required");
        }
    }
    public class StoreBasketHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto)
        : ICommandHandler<StoreBaskeCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBaskeCommand command, CancellationToken cancellationToken)
        {
            //for discount
            await DeductDiscount(command.Cart, cancellationToken);

            //add the cart in DataBase
            ShoppingCart cart = command.Cart;

            var basket = await repository.StoreBasket(cart, cancellationToken);

            return new StoreBasketResult(basket.Username);
        }

        public async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
            //communicate with discout.grpc and get the latest discount & prices of products

            foreach (var item in cart.Items)
            {
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;
            }
        }
    }
}
