using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Entities;

public class DebitAccount : IAccount
{
    private const double MinComission = 0;
    private const double MinMoneyAmount = 0;
    private const double DaysInAYear = 365;

    public DebitAccount(IClient client, double interest)
    {
        if (client is null)
        {
            throw new BanksException($"Failed to construct DebitAccount, given value: client  can not be null");
        }

        if (interest <= MinComission)
        {
            throw new BanksException($"Failed to construct DebitAccount, given value: interest {interest}  can not be <= {MinComission}");
        }

        Id = Guid.NewGuid();
        Client = client;
        Money = MinMoneyAmount;
        SavedMoney = MinMoneyAmount;
        Comission = MinComission;
        Interest = interest;
        DailyInterest = interest / DaysInAYear;
    }

    public Guid Id { get; }
    public double SavedMoney { get; private set; }
    public double Interest { get; }
    public double DailyInterest { get; }
    public IClient Client { get; }
    public double Money { get; set; }
    public double Comission { get; }

    public void Replenish(double amount)
    {
        if (amount <= MinMoneyAmount)
        {
            throw new BanksException($"Failed to Replenish account {this}, given value: amount {amount}  can not be <= {MinMoneyAmount}");
        }

        Money += amount;
    }

    public void Withdraw(double amount)
    {
        if (amount <= MinMoneyAmount)
        {
            throw new BanksException($"Failed to Withdraw account {this}, given value: amount {amount}  can not be <= {MinMoneyAmount}");
        }

        if (Money - amount < MinMoneyAmount)
        {
            throw new BanksException($"Failed to Withdraw from account {this}. Account doesn't have enough money");
        }

        Money -= amount;
    }

    public void AddDailyComission()
    {
        SavedMoney += Money * DailyInterest;
    }

    public void AddMonthlyComission()
    {
        Money += SavedMoney;
        SavedMoney = 0;
    }
}
