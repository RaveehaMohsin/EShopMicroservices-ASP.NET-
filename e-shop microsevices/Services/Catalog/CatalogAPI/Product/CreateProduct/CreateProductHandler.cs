


namespace CatalogAPI.Product.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description , string Imagefile , decimal Price) :
        ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
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
