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
    public class StoreBasketHandler(IBasketRepository repository) : ICommandHandler<StoreBaskeCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBaskeCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart cart = command.Cart;

            var basket = await repository.StoreBasket(cart , cancellationToken);

            return new StoreBasketResult(basket.Username);
        }
    }
}
