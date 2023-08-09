namespace Shops.Interfaces
{
    public interface ICashAccount
    {
        double Money { get; }
        void AddMoney(double amount);
        void SubstractMoney(double amount);
        void SendMoneyTo(ICashAccount cashAccount, double amount);
    }
}