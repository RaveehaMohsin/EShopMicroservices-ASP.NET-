using System.Linq;

namespace BasketAPI.Models
{
    public class ShoppingCart
    {
        public string Username { get; set; } = default!;
        public List<ShoppingCartItem> Items { get; set; } = new();
        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

        public ShoppingCart(string username)
        {
            Username = username;
        }

        //for mapping
        public ShoppingCart()
        {

        }


    }
}
