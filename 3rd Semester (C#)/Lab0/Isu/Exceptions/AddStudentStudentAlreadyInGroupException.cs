using System.Runtime.Serialization;
namespace Isu.Exceptions;

public class AddStudentStudentAlreadyInGroupException : ApplicationException
{
    public AddStudentStudentAlreadyInGroupException() { }

    public AddStudentStudentAlreadyInGroupException(string message)
        : base(message) { }

    public AddStudentStudentAlreadyInGroupException(string message, Exception inner)
        : base(message, inner) { }

    protected AddStudentStudentAlreadyInGroupException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}