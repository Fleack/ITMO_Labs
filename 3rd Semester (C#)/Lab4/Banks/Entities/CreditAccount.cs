using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Entities;

public class CreditAccount : IAccount
{
    private const double MinMoneyAmount = 0;
    private const double MinLoanComission = 0;

    public CreditAccount(IClient client, double comission)
    {
        if (client is null)
        {
            throw new BanksException($"Failed to construct client, given value: client  can not be null");
        }

        if (comission <= MinLoanComission)
        {
            throw new BanksException($"Failed to construct client, given value: loanInterest {comission} can not be less or equal {MinLoanComission}");
        }

        Id = Guid.NewGuid();
        Client = client;
        Comission = comission;
        Money = 0;
    }

    public Guid Id { get; }
    public IClient Client { get; }
    public double Money { get; set; }
    public double Comission { get; }

    public void Replenish(double amount)
    {
        if (amount <= MinMoneyAmount)
        {
            throw new BanksException($"Failed to Withdraw account {this}, given value: amount {amount}  can not be <= {MinMoneyAmount}");
        }

        Money += amount;
    }

    public void Withdraw(double amount)
    {
        if (amount <= MinMoneyAmount)
        {
            throw new BanksException($"Failed to Replenish account {this}, given value: amount {amount}  can not be <= {MinMoneyAmount}");
        }

        if (Money - amount < MinMoneyAmount)
        {
            Money -= Comission;
        }

        Money -= amount;
    }

    public void AddDailyComission() { }
    public void AddMonthlyComission() { }
}
