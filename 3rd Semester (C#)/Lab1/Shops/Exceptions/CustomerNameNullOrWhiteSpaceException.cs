using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class CustomerNameNullOrWhiteSpaceException : ApplicationException
{
    public CustomerNameNullOrWhiteSpaceException() { }

    public CustomerNameNullOrWhiteSpaceException(string message)
        : base(message) { }

    public CustomerNameNullOrWhiteSpaceException(string message, Exception inner)
        : base(message, inner) { }

    protected CustomerNameNullOrWhiteSpaceException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}