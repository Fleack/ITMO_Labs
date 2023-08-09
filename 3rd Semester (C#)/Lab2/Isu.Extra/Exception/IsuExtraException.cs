using System.Runtime.Serialization;
namespace Isu.Exceptions;

public class IsuExtraException : ApplicationException
{
    public IsuExtraException() { }

    public IsuExtraException(string message)
        : base(message) { }

    public IsuExtraException(string message, Exception inner)
        : base(message, inner) { }

    protected IsuExtraException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}