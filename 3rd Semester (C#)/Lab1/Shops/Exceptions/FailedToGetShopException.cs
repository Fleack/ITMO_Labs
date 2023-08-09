using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class FailedToGetShopException : ApplicationException
{
    public FailedToGetShopException() { }

    public FailedToGetShopException(string message)
        : base(message) { }

    public FailedToGetShopException(string message, Exception inner)
        : base(message, inner) { }

    protected FailedToGetShopException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}