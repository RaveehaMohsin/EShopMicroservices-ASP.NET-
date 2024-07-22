
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace BasketAPI.Data
{
    public class CashedBasketRepository(IBasketRepository repository , IDistributedCache cache) : IBasketRepository
    {
       
        public async Task<ShoppingCart> GetBasket(string Username, CancellationToken cancellationToken = default)
        {
            var cachedbasket = await cache.GetStringAsync(Username, cancellationToken);
            if (!string.IsNullOrEmpty(cachedbasket)) //find in cache
            { 
               return JsonSerializer.Deserialize<ShoppingCart>(cachedbasket)!;
            }
            var basket = await repository.GetBasket(Username, cancellationToken); //if not in cache then find in db
            await cache.SetStringAsync(Username , JsonSerializer.Serialize(basket) , cancellationToken);
            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
            await repository.StoreBasket(cart, cancellationToken); //save in database
            await cache.SetStringAsync(cart.Username, JsonSerializer.Serialize(cart), cancellationToken); //save in cache
            return cart;
        }

        public async Task<bool> DeleteBasket(string Username, CancellationToken cancellationToken = default)
        {
            await repository.DeleteBasket(Username, cancellationToken); //delete from db
            await cache.RemoveAsync(Username, cancellationToken); //delete from cache too
            return true;
        }
    }
}
