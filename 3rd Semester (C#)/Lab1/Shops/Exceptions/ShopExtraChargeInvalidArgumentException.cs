using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class ShopExtraChargeInvalidArgumentException : ApplicationException
{
    public ShopExtraChargeInvalidArgumentException() { }

    public ShopExtraChargeInvalidArgumentException(string message)
        : base(message) { }

    public ShopExtraChargeInvalidArgumentException(string message, Exception inner)
        : base(message, inner) { }

    protected ShopExtraChargeInvalidArgumentException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}