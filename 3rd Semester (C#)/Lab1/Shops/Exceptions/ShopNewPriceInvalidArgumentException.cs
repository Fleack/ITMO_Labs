using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class ShopNewPriceInvalidArgumentException : ApplicationException
{
    public ShopNewPriceInvalidArgumentException() { }

    public ShopNewPriceInvalidArgumentException(string message)
        : base(message) { }

    public ShopNewPriceInvalidArgumentException(string message, Exception inner)
        : base(message, inner) { }

    protected ShopNewPriceInvalidArgumentException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}