using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class ShopStorageProductsNullReferenceException : ApplicationException
{
    public ShopStorageProductsNullReferenceException() { }

    public ShopStorageProductsNullReferenceException(string message)
        : base(message) { }

    public ShopStorageProductsNullReferenceException(string message, Exception inner)
        : base(message, inner) { }

    protected ShopStorageProductsNullReferenceException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}