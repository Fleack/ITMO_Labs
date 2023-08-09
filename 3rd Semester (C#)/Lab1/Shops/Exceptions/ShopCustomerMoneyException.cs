using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class ShopCustomerMoneyException : ApplicationException
{
    public ShopCustomerMoneyException() { }

    public ShopCustomerMoneyException(string message)
        : base(message) { }

    public ShopCustomerMoneyException(string message, Exception inner)
        : base(message, inner) { }

    protected ShopCustomerMoneyException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}