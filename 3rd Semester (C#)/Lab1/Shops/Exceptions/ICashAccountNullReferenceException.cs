using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class ICashAccountNullReferenceException : ApplicationException
{
    public ICashAccountNullReferenceException() { }

    public ICashAccountNullReferenceException(string message)
        : base(message) { }

    public ICashAccountNullReferenceException(string message, Exception inner)
        : base(message, inner) { }

    protected ICashAccountNullReferenceException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}