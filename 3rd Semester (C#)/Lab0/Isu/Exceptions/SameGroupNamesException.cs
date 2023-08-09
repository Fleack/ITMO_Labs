using System.Runtime.Serialization;
namespace Isu.Exceptions;

public class SameGroupNamesException : ApplicationException
{
    public SameGroupNamesException() { }

    public SameGroupNamesException(string message)
        : base(message) { }

    public SameGroupNamesException(string message, Exception inner)
        : base(message, inner) { }

    protected SameGroupNamesException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}