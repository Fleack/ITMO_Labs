using System.Runtime.Serialization;
namespace Isu.Exceptions;

public class StudentsNullReferenceException : ApplicationException
{
    public StudentsNullReferenceException() { }

    public StudentsNullReferenceException(string message)
        : base(message) { }

    public StudentsNullReferenceException(string message, Exception inner)
        : base(message, inner) { }

    protected StudentsNullReferenceException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}