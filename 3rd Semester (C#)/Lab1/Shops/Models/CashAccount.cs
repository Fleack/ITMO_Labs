using Shops.Exceptions;
using Shops.Interfaces;

namespace Shops.Models
{
    public class CashAccount : ICashAccount
    {
        private const int MinAmountOfMoney = 0;

        public CashAccount(double money_amount)
        {
            if (money_amount < MinAmountOfMoney)
            {
                throw new CashAccountNegativeMoneyAmountException($"Failed to construct CashAccount, money_amount: {money_amount} can not be negative");
            }

            Money = money_amount;
        }

        public double Money { get; private set; }

        public void AddMoney(double amount)
        {
            if (amount < MinAmountOfMoney)
            {
                throw new CashAccountNegativeMoneyAmountException($"Failed to AddMoney to account: {this}, amount of money can not be 0 or negative");
            }

            Money += amount;
        }

        public void SubstractMoney(double amount)
        {
            if (amount < MinAmountOfMoney)
            {
                throw new CashAccountNegativeMoneyAmountException($"Failed to SubstractMoney to account: {this}, amount of money can not be 0 or negative");
            }

            Money -= amount;
        }

        public void SendMoneyTo(ICashAccount cashAccount, double amount)
        {
            if (cashAccount is null)
            {
                throw new CashAccountAccountNullReferenceException($"Failed to Transfer money, given ICashAccount can not be null");
            }

            if (amount < MinAmountOfMoney)
            {
                throw new CashAccountNegativeMoneyAmountException($"Failed to SubstractMoney to account: {this}, amount of money can not be 0 or negative");
            }

            SubstractMoney(amount);
            cashAccount.AddMoney(amount);
        }
    }
}
