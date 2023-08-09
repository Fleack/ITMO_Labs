using System.Runtime.Serialization;
namespace Isu.Exceptions;

public class GroupCreatingOverflowException : ApplicationException
{
    public GroupCreatingOverflowException() { }

    public GroupCreatingOverflowException(string message)
        : base(message) { }

    public GroupCreatingOverflowException(string message, Exception inner)
        : base(message, inner) { }

    protected GroupCreatingOverflowException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}