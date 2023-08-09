using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class ShopCustomerNullReferenceException : ApplicationException
{
    public ShopCustomerNullReferenceException() { }

    public ShopCustomerNullReferenceException(string message)
        : base(message) { }

    public ShopCustomerNullReferenceException(string message, Exception inner)
        : base(message, inner) { }

    protected ShopCustomerNullReferenceException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}