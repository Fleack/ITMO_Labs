using System.Runtime.Serialization;
namespace Shops.Exceptions;

public class CashAccountNegativeMoneyAmountException : ApplicationException
{
    public CashAccountNegativeMoneyAmountException() { }

    public CashAccountNegativeMoneyAmountException(string message)
        : base(message) { }

    public CashAccountNegativeMoneyAmountException(string message, Exception inner)
        : base(message, inner) { }

    protected CashAccountNegativeMoneyAmountException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}