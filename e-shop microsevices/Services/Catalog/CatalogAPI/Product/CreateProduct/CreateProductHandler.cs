namespace CatalogAPI.Product.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string Imagefile, decimal Price) :
        ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.Imagefile).NotEmpty().WithMessage("Image file is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price is required");
        }
    }

    internal class CreateProductHandler(IDocumentSession session)// ,IValidator<CreateProductCommand> validator)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {

            //for validation (BUT IT IS NOT A GOOD APPROACH TO INJECT IVALIDATOR HERE AND TO MAKE THIS REPETETIVE)
            //var result = await validator.ValidateAsync(command, cancellationToken);
            //var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
            //if (errors.Any())
            //{
            //    throw new ValidationException(errors.FirstOrDefault());
            //}



            //Business logic to create a product
            //create product from command
            var product = new Models.Product
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                Imagefile = command.Imagefile,
                Price = command.Price

            };

            // save to database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            //return createproductresult result

            return new CreateProductResult(product.Id);

        }
    }
}

