using Banks.Entities;
using Banks.Tools;
using Xunit;

namespace Banks.Test;

public class BanksTests
{
    private readonly CentralBank _centralBank = CentralBank.GetCentralBank();

    [Fact]
    public void BankCreation()
    {
        const string name = "Bank1";
        const double depositInterest = 10;
        const double creditComission = 100;
        const double debitComission = 5;
        const double doubtfulClientLimit = 50000;

        Bank new_bank = _centralBank.CreateNewBank(name, depositInterest, creditComission, debitComission, doubtfulClientLimit);

        Assert.Contains(new_bank, _centralBank.Banks);
        Assert.Equal(new_bank.Name, name);
        Assert.Equal(new_bank.DepositInterest, depositInterest);
        Assert.Equal(new_bank.CreditComission, creditComission);
        Assert.Equal(new_bank.DebitComission, debitComission);
        Assert.Equal(new_bank.DoubtfulClientLimit, doubtfulClientLimit);
    }

    [Fact]
    public void CreatingClientAndAddingToBank()
    {
        const string bank_name = "Bank2";
        const double depositInterest = 10;
        const double creditComission = 100;
        const double debitComission = 5;
        const double doubtfulClientLimit = 50000;

        Bank bank = _centralBank.CreateNewBank(bank_name, depositInterest, creditComission, debitComission, doubtfulClientLimit);

        const string client_name = "Ivan";
        const string client_surname = "Ivanov";

        ClientBuilder clientBuilder = new ();
        clientBuilder.SetFullName(new Models.FullName(client_name, client_surname));
        Client client = clientBuilder.CreateClient();

        bank.AddNewClient(client);

        Assert.Equal(client_name, client.FullName.Name);
        Assert.Equal(client_surname, client.FullName.Surname);
        Assert.Null(client.PassportData);
        Assert.Null(client.Address);
        Assert.Contains(client, bank.Clients);
    }

    [Fact]
    public void CreatingClientsAccounts()
    {
        const string bank_name = "Bank3";
        const double depositInterest = 10;
        const double creditComission = 100;
        const double debitComission = 5;
        const double doubtfulClientLimit = 50000;

        Bank bank = _centralBank.CreateNewBank(bank_name, depositInterest, creditComission, debitComission, doubtfulClientLimit);

        const string client_name = "Ivan";
        const string client_surname = "Ivanov";

        ClientBuilder clientBuilder = new ();
        clientBuilder.SetFullName(new Models.FullName(client_name, client_surname));
        Client client = clientBuilder.CreateClient();

        bank.AddNewClient(client);

        var deposit_period = TimeSpan.FromDays(60);
        const double money_amount = 100000;
        DebitAccount debitAccount = bank.CreateDebitAccount(client.Id);
        CreditAccount creditAccount = bank.CreateCreditAccount(client.Id);
        DepositAccount depositAccount = bank.CreateDepositAccount(client.Id, deposit_period, money_amount);

        Assert.Contains(debitAccount, client.Accounts);
        Assert.Contains(creditAccount, client.Accounts);
        Assert.Contains(depositAccount, client.Accounts);
    }

    [Fact]
    public void ReplenishFromAccount()
    {
        const string bank_name = "Bank4";
        const double depositInterest = 10;
        const double creditComission = 100;
        const double debitComission = 5;
        const double doubtfulClientLimit = 50000;

        Bank bank = _centralBank.CreateNewBank(bank_name, depositInterest, creditComission, debitComission, doubtfulClientLimit);

        const string client_name = "Ivan";
        const string client_surname = "Ivanov";

        ClientBuilder clientBuilder = new ();
        clientBuilder.SetFullName(new Models.FullName(client_name, client_surname));
        Client client = clientBuilder.CreateClient();

        bank.AddNewClient(client);

        const double money_amount = 100000;
        const double money_amount_twice = 200000;
        const double ZeroMoney = 0;
        DebitAccount debitAccount = bank.CreateDebitAccount(client.Id);

        Assert.Equal(debitAccount.Money, ZeroMoney);

        _centralBank.ReplenishAccount(debitAccount.Id, money_amount);
        Assert.Equal(debitAccount.Money, money_amount);

        _centralBank.ReplenishAccount(debitAccount.Id, money_amount);
        Assert.Equal(debitAccount.Money, money_amount_twice);
    }

    [Fact]
    public void WithdrawFromAccount()
    {
        const string bank_name = "Bank5";
        const double depositInterest = 10;
        const double creditComission = 100;
        const double debitComission = 5;
        const double doubtfulClientLimit = 50000;

        Bank bank = _centralBank.CreateNewBank(bank_name, depositInterest, creditComission, debitComission, doubtfulClientLimit);

        const string client_name = "Ivan";
        const string client_surname = "Ivanov";

        ClientBuilder clientBuilder = new ();
        clientBuilder.SetFullName(new Models.FullName(client_name, client_surname));
        Client client = clientBuilder.CreateClient();

        bank.AddNewClient(client);

        const double repl_money_amount = 100000;
        const double withdraw_money_amount = 40000;
        const double expected_money_left = repl_money_amount - withdraw_money_amount;

        DebitAccount debitAccount = bank.CreateDebitAccount(client.Id);
        _centralBank.ReplenishAccount(debitAccount.Id, repl_money_amount);

        _centralBank.WithdrawFromAccount(debitAccount.Id, withdraw_money_amount);
        Assert.Equal(expected_money_left, debitAccount.Money);
    }

    [Fact]
    public void TransferBetweenAccounts()
    {
        const string bank_name = "Bank6";
        const double depositInterest = 10;
        const double creditComission = 100;
        const double debitComission = 5;
        const double doubtfulClientLimit = 50000;

        Bank bank = _centralBank.CreateNewBank(bank_name, depositInterest, creditComission, debitComission, doubtfulClientLimit);

        const string client1_name = "Ivan";
        const string client1_surname = "Ivanov";

        ClientBuilder client1Builder = new ();
        client1Builder.SetFullName(new Models.FullName(client1_name, client1_surname));
        Client client1 = client1Builder.CreateClient();

        const string client2_name = "Name";
        const string client2_surname = "Surname";

        ClientBuilder client2Builder = new ();
        client2Builder.SetFullName(new Models.FullName(client2_name, client2_surname));
        Client client2 = client2Builder.CreateClient();

        bank.AddNewClient(client1);
        bank.AddNewClient(client2);

        const double repl_money_amount = 100000;
        const double transfer_money_amount = 30000;
        const double expected_money_left = repl_money_amount - transfer_money_amount;

        DebitAccount debitAccount1 = bank.CreateDebitAccount(client1.Id);
        DebitAccount debitAccount2 = bank.CreateDebitAccount(client2.Id);
        _centralBank.ReplenishAccount(debitAccount1.Id, repl_money_amount);

        _centralBank.TransferMoneyBetweenAccounts(debitAccount1.Id, debitAccount2.Id, transfer_money_amount);
        Assert.Equal(expected_money_left, debitAccount1.Money);
        Assert.Equal(transfer_money_amount, debitAccount2.Money);
    }
}
