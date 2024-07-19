
namespace buildingblocks.Exceptions
{
    public class internalserverexception : Exception
    {
        public string? Details { get; }
        public internalserverexception(string message ) : base(message) { }

        public internalserverexception(string message , string details) : base(message) 
        {
            Details = details;
        }

    }
}
