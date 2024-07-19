
namespace CatalogAPI.Product.UpdateProduct
{
    public record UpdateProductCommand(Guid Id,string Name, List<string> Category, string Description, string Imagefile, decimal Price) : ICommand<UpdateProductResult>;
    
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {

            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2,150).WithMessage("Name must be between 2 and 150 characters");
           
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price is required");
        }
    }
    internal class UpdateProductHandler (IDocumentSession session , ILogger <UpdateProductHandler> logger)
      : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductHandler.Handle called with {@Command}", command);

            var product = await  session.LoadAsync<Models.Product>(command.Id, cancellationToken);
            if (product == null) 
            {
                throw new productnotfoundexception(command.Id);
            }
            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.Imagefile = command.Imagefile;
            product.Price = command.Price;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}
