using System.Runtime.Serialization;

namespace Banks.Tools;

public class BanksException : Exception
{
    public BanksException() { }

    public BanksException(string message)
        : base(message) { }

    public BanksException(string message, Exception inner)
        : base(message, inner) { }

    protected BanksException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
