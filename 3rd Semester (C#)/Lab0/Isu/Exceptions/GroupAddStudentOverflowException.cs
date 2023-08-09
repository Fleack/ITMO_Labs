using System.Runtime.Serialization;
namespace Isu.Exceptions;

public class GroupAddStudentOverflowException : ApplicationException
{
    public GroupAddStudentOverflowException() { }

    public GroupAddStudentOverflowException(string message)
        : base(message) { }

    public GroupAddStudentOverflowException(string message, Exception inner)
        : base(message, inner) { }

    protected GroupAddStudentOverflowException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}