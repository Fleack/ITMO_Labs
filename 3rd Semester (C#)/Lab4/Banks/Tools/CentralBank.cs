using Banks.Entities;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Tools;

public class CentralBank
{
    private const int FirstDayOfMonth = 1;

    private static CentralBank? _centralBank = null;
    private List<Bank> _banks = new List<Bank>();
    private List<ITransaction> _transactions = new List<ITransaction>();
    private DateTime _date = DateTime.Now;

    private CentralBank()
    { }

    public IReadOnlyCollection<Bank> Banks => _banks;
    public IReadOnlyCollection<ITransaction> Transactions => _transactions;

    public static CentralBank GetCentralBank()
    {
        if (_centralBank == null)
        {
            _centralBank = new CentralBank();
        }

        return _centralBank;
    }

    public Bank CreateNewBank(
        string name,
        double depositInterest,
        double creditComission,
        double debitComission,
        double doubtfulClientLimit)
    {
        if (_banks.Any(bank => bank.Name == name))
        {
            throw new BanksException($"Failed to CreateNewBank, Bank with name: {name} already exists");
        }

        Bank bank = new (name, depositInterest, creditComission, debitComission, doubtfulClientLimit);
        _banks.Add(bank);

        return bank;
    }

    public void AddNewBank(Bank new_bank)
    {
        if (_banks.Any(bank => bank.Name == new_bank.Name))
        {
            throw new BanksException($"Failed to CreateNewBank, Bank with name: {new_bank.Name} already exists");
        }

        _banks.Add(new_bank);
    }

    public void WithdrawFromAccount(Guid account_id, double amount)
    {
        GetAccountBank(account_id).WithdrawFromAccount(account_id, amount);
        FormWithdrawTransaction(GetAccountByID(account_id), amount, GetAccountByID(account_id).Comission);
    }

    public void ReplenishAccount(Guid account_id, double amount)
    {
        GetAccountBank(account_id).ReplenishAccount(account_id, amount);
        FormReplenishmentTransaction(GetAccountByID(account_id), amount, GetAccountByID(account_id).Comission);
    }

    public void TransferMoneyBetweenAccounts(Guid account_from_id, Guid account_to_id, double amount)
    {
        GetAccountBank(account_to_id).ReplenishAccount(account_to_id, amount);
        GetAccountBank(account_from_id).WithdrawFromAccount(account_from_id, amount);
        FormTransferTransaction(GetAccountByID(account_from_id), GetAccountByID(account_to_id), amount, GetAccountByID(account_from_id).Comission);
    }

    public void CancelTransaction(Guid transaction_id)
    {
        ITransaction? transaction = _transactions.FirstOrDefault(tr => tr.Id.Equals(transaction_id));
        if (transaction is null)
        {
            throw new BanksException($"Failed to CancelTransaction, transaction {transaction} does not exist or already canceled");
        }

        transaction.Cancel();
        _transactions.Remove(transaction);
    }

    public void SetNextDay()
    {
        _date.AddDays(1);
        BanksNotification();
    }

    public void TimeAccelerator(int days_amount)
    {
        for (int i = 0; i < days_amount; i++)
        {
            SetNextDay();
        }
    }

    public IAccount GetAccountByID(Guid account_id)
    {
        foreach (IAccount? account in _banks.SelectMany(bank => bank.Clients.SelectMany(client => client.Accounts.Where(account => account.Id.Equals(account_id)))))
        {
            return account;
        }

        throw new BanksException($"Failed to GetAccountByID, account with id: {account_id} doesn't exist");
    }

    public Bank GetBankByID(Guid bank_id)
    {
        foreach (Bank? bank in _banks.Where(bank => bank.Id.Equals(bank_id)))
        {
            return bank;
        }

        throw new BanksException($"Failed to GetBankByID, bank with id: {bank_id} doesn't exist");
    }

    private void BanksNotification()
    {
        DailyBanksNotification();
        if (_date.Day == FirstDayOfMonth)
            MonthlyBanksNotification();
    }

    private void MonthlyBanksNotification()
    {
        foreach (Bank bank in _banks)
        {
            bank.MonthlyAccountNotification();
        }
    }

    private void DailyBanksNotification()
    {
        foreach (Bank bank in _banks)
        {
            bank.DailyAccountNotification();
        }
    }

    private Bank GetAccountBank(Guid account_id)
    {
        foreach (Bank? bank in _banks.SelectMany(bank => bank.Clients.SelectMany(client => client.Accounts.Where(account => account.Id.Equals(account_id)).Select(account => bank))))
        {
            return bank;
        }

        throw new BanksException($"Failed to GetAccountBank, Bank of account with id: {account_id} doesn't exist");
    }

    private void FormWithdrawTransaction(IAccount account, double amount, double comission)
    {
        WithdrawTransaction transaction = new (account, amount, comission, _date);
        _transactions.Add(transaction);
    }

    private void FormReplenishmentTransaction(IAccount account, double amount, double comission)
    {
        ReplenishmentTransaction transaction = new (account, amount, comission, _date);
        _transactions.Add(transaction);
    }

    private void FormTransferTransaction(IAccount account_from, IAccount account_to, double amount, double comission)
    {
        TransferTransaction transaction = new (account_from, account_to, amount, comission, _date);
        _transactions.Add(transaction);
    }
}
