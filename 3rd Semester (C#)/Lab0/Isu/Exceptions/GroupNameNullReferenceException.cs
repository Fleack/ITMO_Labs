using System.Runtime.Serialization;
namespace Isu.Exceptions;

public class GroupNameNullReferenceException : ApplicationException
{
    public GroupNameNullReferenceException() { }

    public GroupNameNullReferenceException(string message)
        : base(message) { }

    public GroupNameNullReferenceException(string message, Exception inner)
        : base(message, inner) { }

    protected GroupNameNullReferenceException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}