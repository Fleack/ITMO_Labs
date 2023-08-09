using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class ShopProvideNullReferenceException : ApplicationException
{
    public ShopProvideNullReferenceException() { }

    public ShopProvideNullReferenceException(string message)
        : base(message) { }

    public ShopProvideNullReferenceException(string message, Exception inner)
        : base(message, inner) { }

    protected ShopProvideNullReferenceException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}