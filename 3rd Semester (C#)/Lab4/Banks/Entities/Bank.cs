using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Entities;

public class Bank
{
    private const double MinComissionsValue = 0;

    private List<IClient> _clients;

    public Bank(
        string name,
        double depositInterest,
        double creditComission,
        double debitComission,
        double doubtfulClientLimit)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BanksException($"Failed to construct Bank, given value: name {name} can not be null or white space");
        }

        if (depositInterest < MinComissionsValue)
        {
            throw new BanksException($"Failed to construct Bank, given value: depositInterest {depositInterest} can not <= {MinComissionsValue}");
        }

        if (creditComission < MinComissionsValue)
        {
            throw new BanksException($"Failed to construct Bank, given value: creditComission {creditComission} can not <= {MinComissionsValue}");
        }

        if (debitComission < MinComissionsValue)
        {
            throw new BanksException($"Failed to construct Bank, given value: debitComission {debitComission} can not <= {MinComissionsValue}");
        }

        Name = name;
        Id = Guid.NewGuid();
        _clients = new List<IClient>();
        DepositInterest = depositInterest;
        CreditComission = creditComission;
        DebitComission = debitComission;
        DoubtfulClientLimit = doubtfulClientLimit;
    }

    public string Name { get; }
    public Guid Id { get; }
    public IReadOnlyList<IClient> Clients => _clients;
    public double DepositInterest { get; private set; }
    public double CreditComission { get; private set; }
    public double DebitComission { get; private set; }
    public double DoubtfulClientLimit { get; private set; }

    public void AddNewClient(IClient new_client)
    {
        if (_clients.Any(client => client.PassportData == new_client.PassportData && new_client.PassportData is not null))
        {
            throw new BanksException($"Failed to AddNewClient, client with same PassportData {new_client.PassportData} already exists");
        }

        _clients.Add(new_client);
    }

    public void RemoveClient(IClient client)
    {
        _clients.Remove(client);
    }

    public void WithdrawFromAccount(Guid account_id, double amount)
    {
        IAccount account_from = GetAccountByID(account_id);
        IClient account_owner = GetAccountOwnerByAccountID(account_id);

        if (!account_owner.ConfirmedStatus &&
            DoubtfulClientLimit > 0 &&
            amount > DoubtfulClientLimit)
        {
            throw new BanksException($"Failed to WithdrawFromAccount, client: {account_from} has limit: {DoubtfulClientLimit} for Withdraws and Transfers");
        }

        account_from.Withdraw(amount);
    }

    public CreditAccount CreateCreditAccount(Guid client_id)
    {
        IClient client = GetClientByID(client_id);
        CreditAccount creditAccount = new (client, CreditComission);
        client.AddNewAccount(creditAccount);

        return creditAccount;
    }

    public DebitAccount CreateDebitAccount(Guid client_id)
    {
        IClient client = GetClientByID(client_id);
        DebitAccount debitAccount = new (client, DebitComission);
        client.AddNewAccount(debitAccount);

        return debitAccount;
    }

    public DepositAccount CreateDepositAccount(Guid client_id, TimeSpan deposit_period, double money_amount)
    {
        IClient client = GetClientByID(client_id);
        DepositAccount depositAccount = new (client, deposit_period, money_amount, DepositInterest);
        client.AddNewAccount(depositAccount);

        return depositAccount;
    }

    public void ReplenishAccount(Guid account_id, double amount)
    {
        IAccount account_to = GetAccountByID(account_id);
        account_to.Replenish(amount);
    }

    public void SetDepositInterest(double new_value)
    {
        if (new_value < MinComissionsValue)
        {
            throw new BanksException($"Given value: depositInterest {new_value} can not <= {MinComissionsValue}");
        }

        DepositInterest = new_value;
        NotifySubscribedClients();
    }

    public void SetCreditComission(double new_value)
    {
        if (new_value < MinComissionsValue)
        {
            throw new BanksException($"Given value: depositInterest {new_value} can not <= {MinComissionsValue}");
        }

        CreditComission = new_value;
        NotifySubscribedClients();
    }

    public void SetDebitComission(double new_value)
    {
        if (new_value < MinComissionsValue)
        {
            throw new BanksException($"Given value: depositInterest {new_value} can not <= {MinComissionsValue}");
        }

        DebitComission = new_value;
        NotifySubscribedClients();
    }

    public void SetDoubtfulClientLimit(double new_value)
    {
        if (new_value < MinComissionsValue)
        {
            throw new BanksException($"Given value: depositInterest {new_value} can not <= {MinComissionsValue}");
        }

        DoubtfulClientLimit = new_value;
        NotifySubscribedClients();
    }

    public void DailyAccountNotification()
    {
        foreach (IClient client in _clients)
        {
            foreach (IAccount account in client.Accounts)
            {
                account.AddDailyComission();
            }
        }
    }

    public void MonthlyAccountNotification()
    {
        foreach (IClient client in _clients)
        {
            foreach (IAccount account in client.Accounts)
            {
                account.AddMonthlyComission();
            }
        }
    }

    private void NotifySubscribedClients()
    {
        foreach (IClient client in _clients)
        {
            if (client.ChangesSubscription)
            {
                client.AccountRulesChanged();
            }
        }
    }

    private IAccount GetAccountByID(Guid account_id)
    {
        foreach (IAccount? account in _clients.SelectMany(client => client.Accounts.Where(account => account.Id.Equals(account_id))))
        {
            return account;
        }

        throw new BanksException($"Failed to FindAccountByID, account with given id: {account_id} doesn't exist");
    }

    private IClient GetAccountOwnerByAccountID(Guid account_id)
    {
        IClient? account_owner = _clients.SingleOrDefault(client => client.Accounts.Any(account => account.Id.Equals(account_id)));

        if (account_owner is null)
        {
            throw new BanksException($"Failed to GetAccountOwnerByAccountID, account_owner with given id: {account_id} doesn't exist");
        }

        return account_owner;
    }

    private IClient GetClientByID(Guid client_id)
    {
        IClient? account = _clients.SingleOrDefault(client => client.Id.Equals(client_id));

        if (account is null)
        {
            throw new BanksException($"Failed to GetClientID, client_id with given id: {client_id} doesn't exist");
        }

        return account;
    }
}
