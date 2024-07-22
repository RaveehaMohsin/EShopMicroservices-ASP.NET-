namespace BasketAPI.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
   
        public async Task<ShoppingCart> GetBasket(string Username, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(Username , cancellationToken);

            return basket is null ? throw new Basketnotfoundexception(Username) : basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
           session.Store(cart);
           await session.SaveChangesAsync(cancellationToken);
           return cart;
        }

        public async Task<bool> DeleteBasket(string Username, CancellationToken cancellationToken = default)
        {
            session.Delete<ShoppingCart>(Username);
            await session.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
