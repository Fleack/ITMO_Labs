using System.Runtime.Serialization;
namespace Isu.Exceptions;

public class StudentNullReferenceException : ApplicationException
{
    public StudentNullReferenceException() { }

    public StudentNullReferenceException(string message)
        : base(message) { }

    public StudentNullReferenceException(string message, Exception inner)
        : base(message, inner) { }

    protected StudentNullReferenceException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}