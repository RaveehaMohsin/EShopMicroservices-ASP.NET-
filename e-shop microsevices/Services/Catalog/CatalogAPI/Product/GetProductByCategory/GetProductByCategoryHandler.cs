
namespace CatalogAPI.Product.GetProductByCategory
{
    public record GetProductByCategoryQuery(string category) : IQuery<GetProductByCategoryesult>;

    public record GetProductByCategoryesult(IEnumerable <Models.Product>Products);
    internal class GetProductByCategoryHandler(IDocumentSession session, ILogger<GetProductByCategoryHandler> logger)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryesult>
    {
        public async Task<GetProductByCategoryesult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryHandler.Handle called with {@Query}", query);

            var products = await session.Query<Models.Product>().Where(p => p.Category.Contains(query.category)).ToListAsync(cancellationToken);

            return new GetProductByCategoryesult(products);
        }
    }
}
