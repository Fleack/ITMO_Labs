using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class ShopAdressNullReferenceException : ApplicationException
{
    public ShopAdressNullReferenceException() { }

    public ShopAdressNullReferenceException(string message)
        : base(message) { }

    public ShopAdressNullReferenceException(string message, Exception inner)
        : base(message, inner) { }

    protected ShopAdressNullReferenceException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}