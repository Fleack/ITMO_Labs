using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class ShopNotEnoughProductsException : ApplicationException
{
    public ShopNotEnoughProductsException() { }

    public ShopNotEnoughProductsException(string message)
        : base(message) { }

    public ShopNotEnoughProductsException(string message, Exception inner)
        : base(message, inner) { }

    protected ShopNotEnoughProductsException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}