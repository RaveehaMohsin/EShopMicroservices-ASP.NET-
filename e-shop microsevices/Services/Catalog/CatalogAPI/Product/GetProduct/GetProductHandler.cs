
namespace CatalogAPI.Product.GetProduct
{
    public record GetProductQuery() : IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Models.Product>Products);
    internal class GetProductHandler (IDocumentSession session ,ILogger<GetProductHandler> logger)
    : IQueryHandler<GetProductQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductHandler.Handle called with {@Query}", query);
            var products = await session.Query<Models.Product>().ToListAsync(cancellationToken);
            return new GetProductResult(products);
        }
    }
}
