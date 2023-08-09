using System.Runtime.Serialization;

namespace Backups.Exceptions;

public class BackupsException : ApplicationException
{
    public BackupsException() { }

    public BackupsException(string message)
        : base(message) { }

    public BackupsException(string message, Exception inner)
        : base(message, inner) { }

    protected BackupsException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
