namespace BasketAPI.Data
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string Username , CancellationToken cancellationToken = default);
        Task<ShoppingCart> StoreBasket(ShoppingCart cart , CancellationToken cancellationToken = default);

        Task<bool> DeleteBasket (string Username , CancellationToken cancellationToken = default);
    }
}
