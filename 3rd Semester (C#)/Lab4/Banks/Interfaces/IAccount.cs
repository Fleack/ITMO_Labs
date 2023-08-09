namespace Banks.Interfaces;

public interface IAccount
{
    Guid Id { get; }
    IClient Client { get; }
    double Money { get; internal set; }
    double Comission { get; }

    void Replenish(double amount);
    void Withdraw(double amount);
    void AddDailyComission();
    void AddMonthlyComission();
}
