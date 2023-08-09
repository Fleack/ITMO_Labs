using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class ItemManufacturerNullOrWhiteSpaceException : ApplicationException
{
    public ItemManufacturerNullOrWhiteSpaceException() { }

    public ItemManufacturerNullOrWhiteSpaceException(string message)
        : base(message) { }

    public ItemManufacturerNullOrWhiteSpaceException(string message, Exception inner)
        : base(message, inner) { }

    protected ItemManufacturerNullOrWhiteSpaceException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}