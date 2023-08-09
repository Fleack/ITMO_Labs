using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class FailedToGetCustomerException : ApplicationException
{
    public FailedToGetCustomerException() { }

    public FailedToGetCustomerException(string message)
        : base(message) { }

    public FailedToGetCustomerException(string message, Exception inner)
        : base(message, inner) { }

    protected FailedToGetCustomerException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}