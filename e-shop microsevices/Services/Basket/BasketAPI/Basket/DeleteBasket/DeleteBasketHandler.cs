
namespace BasketAPI.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string Username) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);

    public class deleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public deleteBasketCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username can't be empty");
        }
    }
    public class DeleteBasketHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            //todo deletebasket from session(database)
            await repository.DeleteBasket(command.Username , cancellationToken);

            return new DeleteBasketResult(true);
        }
    }
}
