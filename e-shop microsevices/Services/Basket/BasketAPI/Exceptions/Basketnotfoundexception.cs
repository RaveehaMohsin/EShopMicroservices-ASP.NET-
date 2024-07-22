namespace BasketAPI.Exceptions
{
    public class Basketnotfoundexception : NotFoundException
    {
        public Basketnotfoundexception(string username) : base("Basket" , username)
        {

        }
    }
}
