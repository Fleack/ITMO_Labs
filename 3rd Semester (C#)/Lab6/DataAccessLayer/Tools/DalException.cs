using System.Runtime.Serialization;

namespace DataAccessLayer;

internal class DalException : ApplicationException
{
    internal DalException() { }

    internal DalException(string message)
        : base(message) { }

    internal DalException(string message, Exception inner)
        : base(message, inner) { }

    protected DalException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}