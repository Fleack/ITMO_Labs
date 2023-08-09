using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class ShopItemNullReferenceException : ApplicationException
{
    public ShopItemNullReferenceException() { }

    public ShopItemNullReferenceException(string message)
        : base(message) { }

    public ShopItemNullReferenceException(string message, Exception inner)
        : base(message, inner) { }

    protected ShopItemNullReferenceException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}