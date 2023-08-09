using System.Runtime.Serialization;
namespace Isu.Exceptions;

public class FailedToGetStudentException : ApplicationException
{
    public FailedToGetStudentException() { }

    public FailedToGetStudentException(string message)
        : base(message) { }

    public FailedToGetStudentException(string message, Exception inner)
        : base(message, inner) { }

    protected FailedToGetStudentException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}