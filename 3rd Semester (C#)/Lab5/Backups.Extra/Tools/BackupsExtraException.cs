using System.Runtime.Serialization;

namespace Backups.Extra.Tools;

public class BackupsExtraException : ApplicationException
{
    public BackupsExtraException() { }

    public BackupsExtraException(string message)
        : base(message) { }

    public BackupsExtraException(string message, Exception inner)
        : base(message, inner) { }

    protected BackupsExtraException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
