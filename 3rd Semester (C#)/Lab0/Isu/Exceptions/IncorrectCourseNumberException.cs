using System.Runtime.Serialization;
namespace Isu.Exceptions;

public class IncorrectCourseNumberException : ApplicationException
{
    public IncorrectCourseNumberException() { }

    public IncorrectCourseNumberException(string message)
        : base(message) { }

    public IncorrectCourseNumberException(string message, Exception inner)
        : base(message, inner) { }

    protected IncorrectCourseNumberException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}