using buildingblocks.Exceptions;

namespace CatalogAPI.Exceptions
{
    public class productnotfoundexception : NotFoundException
    {
        public productnotfoundexception(Guid id) : base("Product", id)
        {
          
        }
    }
}
