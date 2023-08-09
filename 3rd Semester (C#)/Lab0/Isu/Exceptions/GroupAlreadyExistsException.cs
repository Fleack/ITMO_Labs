using System.Runtime.Serialization;
namespace Isu.Exceptions;

public class GroupAlreadyExistsException : ApplicationException
{
    public GroupAlreadyExistsException() { }

    public GroupAlreadyExistsException(string message)
        : base(message) { }

    public GroupAlreadyExistsException(string message, Exception inner)
        : base(message, inner) { }

    protected GroupAlreadyExistsException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}