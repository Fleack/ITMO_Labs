namespace Banks.Interfaces;

public interface ITransaction
{
    Guid Id { get; }
    IAccount Account { get; }
    double MoneyAmount { get; }
    double Comission { get; }
    DateTime DateTime { get; }
    bool Canceled { get; }

    void Cancel();
}
