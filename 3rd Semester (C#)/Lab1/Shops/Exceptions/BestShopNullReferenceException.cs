using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class BestShopNullReferenceException : ApplicationException
{
    public BestShopNullReferenceException() { }

    public BestShopNullReferenceException(string message)
        : base(message) { }

    public BestShopNullReferenceException(string message, Exception inner)
        : base(message, inner) { }

    protected BestShopNullReferenceException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}