using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class ShopProductsNullReferenceException : ApplicationException
{
    public ShopProductsNullReferenceException() { }

    public ShopProductsNullReferenceException(string message)
        : base(message) { }

    public ShopProductsNullReferenceException(string message, Exception inner)
        : base(message, inner) { }

    protected ShopProductsNullReferenceException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}