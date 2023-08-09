using System.Runtime.Serialization;

namespace BusinessLayer;

public class BllException : ApplicationException
{
    public BllException() { }

    public BllException(string message)
        : base(message) { }

    public BllException(string message, Exception inner)
        : base(message, inner) { }

    protected BllException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}