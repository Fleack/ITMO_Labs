using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class ShopNameNullOrWhiteSpaceException : ApplicationException
{
    public ShopNameNullOrWhiteSpaceException() { }

    public ShopNameNullOrWhiteSpaceException(string message)
        : base(message) { }

    public ShopNameNullOrWhiteSpaceException(string message, Exception inner)
        : base(message, inner) { }

    protected ShopNameNullOrWhiteSpaceException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}