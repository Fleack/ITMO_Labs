using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class AdressConstructionIncorrectArgumentException : ApplicationException
{
    public AdressConstructionIncorrectArgumentException() { }

    public AdressConstructionIncorrectArgumentException(string message)
        : base(message) { }

    public AdressConstructionIncorrectArgumentException(string message, Exception inner)
        : base(message, inner) { }

    protected AdressConstructionIncorrectArgumentException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}