using System.Runtime.Serialization;
namespace Isu.Exceptions;

public class IncorrectGourpNameException : ApplicationException
{
    public IncorrectGourpNameException() { }

    public IncorrectGourpNameException(string message)
        : base(message) { }

    public IncorrectGourpNameException(string message, Exception inner)
        : base(message, inner) { }

    protected IncorrectGourpNameException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}