using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class ShopDoesNotContainProductException : ApplicationException
{
    public ShopDoesNotContainProductException() { }

    public ShopDoesNotContainProductException(string message)
        : base(message) { }

    public ShopDoesNotContainProductException(string message, Exception inner)
        : base(message, inner) { }

    protected ShopDoesNotContainProductException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}