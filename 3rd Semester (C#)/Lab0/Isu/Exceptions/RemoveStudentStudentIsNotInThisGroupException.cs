using System.Runtime.Serialization;
namespace Isu.Exceptions;

public class RemoveStudentStudentIsNotInThisGroupException : ApplicationException
{
    public RemoveStudentStudentIsNotInThisGroupException() { }

    public RemoveStudentStudentIsNotInThisGroupException(string message)
        : base(message) { }

    public RemoveStudentStudentIsNotInThisGroupException(string message, Exception inner)
        : base(message, inner) { }

    protected RemoveStudentStudentIsNotInThisGroupException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}