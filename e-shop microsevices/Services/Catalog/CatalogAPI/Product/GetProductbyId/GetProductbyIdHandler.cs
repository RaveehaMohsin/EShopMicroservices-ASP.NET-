namespace CatalogAPI.Product.GetProductbyId
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Models.Product Product);
    internal class GetProductbyIdHandler(IDocumentSession session , ILogger <GetProductbyIdHandler> logger ) 
     : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductbyIdHandler.Handle called with {@Query}", query);

            var product = await session.LoadAsync<Models.Product>(query.Id , cancellationToken);

            if (product == null)
            {
                throw new productnotfoundexception(query.Id);
            }

            return new GetProductByIdResult(product);
        }
    }
}
