using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class ItemNameNullOrWhiteSpaceException : ApplicationException
{
    public ItemNameNullOrWhiteSpaceException() { }

    public ItemNameNullOrWhiteSpaceException(string message)
        : base(message) { }

    public ItemNameNullOrWhiteSpaceException(string message, Exception inner)
        : base(message, inner) { }

    protected ItemNameNullOrWhiteSpaceException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}