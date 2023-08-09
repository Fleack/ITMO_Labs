using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class ShopStorageExtraChargeInvalidArgumentException : ApplicationException
{
    public ShopStorageExtraChargeInvalidArgumentException() { }

    public ShopStorageExtraChargeInvalidArgumentException(string message)
        : base(message) { }

    public ShopStorageExtraChargeInvalidArgumentException(string message, Exception inner)
        : base(message, inner) { }

    protected ShopStorageExtraChargeInvalidArgumentException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}