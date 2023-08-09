using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Entities;

public class DepositAccount : IAccount
{
    private const double MinMoneyAmount = 0;
    private const double MinDepositMoney = 0;
    private const double MinDepositInterest = 0;
    private const double MinComission = 0;
    private const double DaysInAYear = 365;

    public DepositAccount(IClient client, TimeSpan depositPeriod, double depositMoney, double depositInterest)
    {
        if (client is null)
        {
            throw new BanksException($"Failed to construct client, given value: client  can not be null");
        }

        if (depositInterest <= MinDepositInterest)
        {
            throw new BanksException($"Failed to construct client, given value: depositInterest {depositInterest} can not be less or equal {MinDepositInterest}");
        }

        if (depositMoney <= MinDepositMoney)
        {
            throw new BanksException($"Failed to construct client, given value: depositMoney {depositMoney} can not be less or equal {MinDepositMoney}");
        }

        Id = Guid.NewGuid();
        Client = client;
        DepositMoney = depositMoney;
        DepositInterest = depositInterest;
        DailyDepositInterest = depositInterest / DaysInAYear;
        DepositPeriod = depositPeriod;
        DepositTime = TimeSpan.Zero;
        OnDeposit = true;
        Money = MinMoneyAmount;
        Comission = MinComission;
    }

    public TimeSpan DepositPeriod { get; }
    public TimeSpan DepositTime { get; }
    public Guid Id { get; }
    public IClient Client { get; }
    public double Money { get; set; }
    public double Comission { get; }
    public double DepositMoney { get; private set; }
    public double DepositInterest { get; }
    public double DailyDepositInterest { get; }
    public bool OnDeposit { get; private set; }

    public void Replenish(double amount)
    {
        if (!OnDeposit)
        {
            throw new BanksException($"Failed to Replenish account {this}, forbiden to replenish from DepositAccount if it's not on deposit");
        }

        if (amount <= MinMoneyAmount)
        {
            throw new BanksException($"Failed to Replenish account {this}, given value: amount {amount}  can not be <= {MinMoneyAmount}");
        }

        Money += amount;
    }

    public void Withdraw(double amount)
    {
        if (OnDeposit)
        {
            throw new BanksException($"Failed to Withdraw account {this}, forbiden to withdraw from DepositAccount if it's still on deposit");
        }

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
        if (!OnDeposit) return;

        Money += DepositMoney * DailyDepositInterest;
        IncreaseDepositTime();
    }

    public void AddMonthlyComission() { }

    private void CloseDepositAccount()
    {
        if (!OnDeposit)
        {
            throw new BanksException($"Failed to CloseDebitAccount from account {this}. DebitAccount already is closed");
        }

        Money += DepositMoney;
        DepositMoney = MinMoneyAmount;
        OnDeposit = false;
    }

    private void IncreaseDepositTime()
    {
        TimeSpan oneDay = new (1, 0, 0, 0);
        DepositTime.Add(oneDay);
        TryToCloseDepositAccount();
    }

    private void TryToCloseDepositAccount()
    {
        if (DepositTime > DepositPeriod)
        {
            throw new BanksException($"Failed to TryToCloseDepositAccount account {this}. DepositTime: {DepositTime} is incorrect!");
        }

        if (DepositTime == DepositPeriod)
        {
            CloseDepositAccount();
        }
    }
}
