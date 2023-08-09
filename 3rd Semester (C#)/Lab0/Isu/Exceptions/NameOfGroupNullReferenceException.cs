using System.Runtime.Serialization;
namespace Isu.Exceptions;

public class NameOfGroupNullReferenceException : ApplicationException
{
    public NameOfGroupNullReferenceException() { }

    public NameOfGroupNullReferenceException(string message)
        : base(message) { }

    public NameOfGroupNullReferenceException(string message, Exception inner)
        : base(message, inner) { }

    protected NameOfGroupNullReferenceException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}