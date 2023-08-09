using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class CashAccountAccountNullReferenceException : ApplicationException
{
    public CashAccountAccountNullReferenceException() { }

    public CashAccountAccountNullReferenceException(string message)
        : base(message) { }

    public CashAccountAccountNullReferenceException(string message, Exception inner)
        : base(message, inner) { }

    protected CashAccountAccountNullReferenceException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}