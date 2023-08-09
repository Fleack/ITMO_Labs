using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class ShopNotEnoughMoneyException : ApplicationException
{
    public ShopNotEnoughMoneyException() { }

    public ShopNotEnoughMoneyException(string message)
        : base(message) { }

    public ShopNotEnoughMoneyException(string message, Exception inner)
        : base(message, inner) { }

    protected ShopNotEnoughMoneyException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}