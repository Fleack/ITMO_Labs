using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Models;

public class WithdrawTransaction : ITransaction
{
    public WithdrawTransaction(IAccount account, double moneyAmount, double comission, DateTime dateTime)
    {
        Id = Guid.NewGuid();
        Account = account;
        MoneyAmount = moneyAmount;
        Comission = comission;
        DateTime = dateTime;
    }

    public Guid Id { get; }
    public IAccount Account { get; }
    public double MoneyAmount { get; }
    public double Comission { get; }
    public DateTime DateTime { get; }
    public bool Canceled { get; private set; }

    public void Cancel()
    {
        if (Canceled)
        {
            throw new BanksException($"Failed to Cancel transaction: {this}, transaction is already canceled!");
        }

        Account.Money += MoneyAmount + Comission;
        Canceled = true;
    }
}
