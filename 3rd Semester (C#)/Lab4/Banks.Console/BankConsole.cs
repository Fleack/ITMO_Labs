using Banks.Entities;
using Banks.Interfaces;
using Banks.Models;
using Banks.Tools;

namespace Banks.BankConsole;

internal class BankConsole
{
    private const double MinComissionValue = 0;

    private CentralBank _centralBank = CentralBank.GetCentralBank();

    public BankConsole()
    {
        StartConsole();
    }

    private void StartConsole()
    {
        Console.WriteLine("Hello, this is console UI for banks system");
        Console.WriteLine("Choose one of these options:");

        while (true)
        {
            StartChooseHelp();
            string? input = Console.ReadLine();

            if (input == "~console" || input == "[~console]" || input == "1")
            {
                ControlConsole();
                Console.Clear();
            }
            else if (input == "~exit" || input == "[~exit]" || input == "2")
            {
                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Incorrect input! Please use one of these commands");
            }
        }
    }

    private void StartChooseHelp()
    {
        Console.WriteLine();
        Console.WriteLine("------------------------------------------------------------------------------------------------------");
        Console.WriteLine("1) To choose control console print - [~console]");
        Console.WriteLine("2) To exit print - [~exit]");
        Console.WriteLine("------------------------------------------------------------------------------------------------------");
    }

    private void ControlConsole()
    {
        Console.Clear();
        while (true)
        {
            Console.WriteLine("Welcome to the control console!");
            Console.WriteLine("Here you can:");
            ControlConsoleHelp();
            Console.WriteLine("Type a number of action you want to do...");
            int num = Convert.ToInt32(Console.ReadLine());
            if (num == 0)
            {
                return;
            }
            else if (num == 1)
            {
                AddBanks();
            }
            else if (num == 2)
            {
                ChangeBanksSettings();
            }
            else if (num == 3)
            {
                AddClientToBank();
            }
            else if (num == 4)
            {
                AccountsManipulating();
            }
            else if (num == 5)
            {
                CancelTransaction();
            }
            else if (num == 6)
            {
                PrintBanksList();
            }
            else if (num == 7)
            {
                PrintClientsList();
            }
            else if (num == 8)
            {
                PrintAccountsList();
            }
            else if (num == 9)
            {
                PrintTransactionsList();
            }
            else
            {
                Console.WriteLine("Incorrect input! Plese use on of these options:");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
    }

    private void AddBanks()
    {
        Console.Clear();

        string name = InputBankName();
        double creditComission = InputCreditComission();
        double debitComission = InputDebitComission();
        double depositInterest = InputDepositInterest();
        double doubtfulClientLimits = InputDoubtfulClientLimits();

        Bank bank = new (name, depositInterest, creditComission, debitComission, doubtfulClientLimits);
        if (ConfirmBankAdding(bank))
        {
            _centralBank.AddNewBank(bank);
        }
    }

    private string InputBankName()
    {
        string? name;
        while (true)
        {
            Console.Write("Enter bank name: ");
            name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Incorrect name, please try again...");
                continue;
            }

            if (_centralBank.Banks.Any(bank => bank.Name == name))
            {
                Console.WriteLine($"Bank with name: {name} already exists, please try again...");
                continue;
            }

            break;
        }

        return name;
    }

    private double InputCreditComission()
    {
        double creditComission;
        while (true)
        {
            Console.Write("Enter bank creditComission: ");
            creditComission = Convert.ToDouble(Console.ReadLine());
            if (creditComission >= MinComissionValue)
                break;
            Console.WriteLine("Incorrect creditComission, please try again...");
        }

        return creditComission;
    }

    private double InputDebitComission()
    {
        double debitComission;
        while (true)
        {
            Console.Write("Enter bank debitComission: ");
            debitComission = Convert.ToDouble(Console.ReadLine());
            if (debitComission >= MinComissionValue)
                break;
            Console.WriteLine("Incorrect debitComission, please try again...");
        }

        return debitComission;
    }

    private double InputDepositInterest()
    {
        double depositInterest;
        while (true)
        {
            Console.Write("Enter bank depositInterest: ");
            depositInterest = Convert.ToDouble(Console.ReadLine());
            if (depositInterest >= MinComissionValue)
                break;
            Console.WriteLine("Incorrect depositInterest, please try again...");
        }

        return depositInterest;
    }

    private double InputDoubtfulClientLimits()
    {
        double doubtfulClientLimits;
        while (true)
        {
            Console.Write("Enter bank doubtfulClientLimits: ");
            doubtfulClientLimits = Convert.ToDouble(Console.ReadLine());
            if (doubtfulClientLimits >= MinComissionValue)
                break;
            Console.WriteLine("Incorrect doubtfulClientLimits, please try again...");
        }

        return doubtfulClientLimits;
    }

    private bool ConfirmBankAdding(Bank bank)
    {
        Console.WriteLine();
        Console.WriteLine("Are you sure you want to create a bank with these data:");
        PrintBankInfo(bank);
        return Confirm();
    }

    private bool Confirm()
    {
        while (true)
        {
            Console.WriteLine("Type [y] to confirm or type [n] to reject");
            string? answer = Console.ReadLine();
            if (answer == "y" || answer == "[y]")
            {
                return true;
            }
            else if (answer == "n" || answer == "[n]")
            {
                return false;
            }
            else
            {
                Console.WriteLine("Incorrect answer!");
            }
        }
    }

    private void ChangeBanksSettings()
    {
        Console.Clear();
        Console.WriteLine("Change Bank Settings");
        Bank bank = FindBankByInputID();

        Console.WriteLine("Choose one of these options:");
        while (true)
        {
            ChangeBanksSettingsHelp();

            int num = Convert.ToInt32(Console.ReadLine());
            if (num == 0)
            {
                return;
            }
            else if (num == 1)
            {
                double new_creditComission = InputCreditComission();
                bank.SetCreditComission(new_creditComission);
            }
            else if (num == 2)
            {
                double new_debitComission = InputDebitComission();
                bank.SetDebitComission(new_debitComission);
            }
            else if (num == 3)
            {
                double new_depositInterest = InputDepositInterest();
                bank.SetDepositInterest(new_depositInterest);
            }
            else if (num == 4)
            {
                double new_doubtfulClientLimits = InputDoubtfulClientLimits();
                bank.SetDoubtfulClientLimit(new_doubtfulClientLimits);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Incorrect input! Plese use on of these options:");
            }
        }
    }

    private void ChangeBanksSettingsHelp()
    {
        Console.WriteLine("0 - Go back");
        Console.WriteLine("1 - Change Credit Comission");
        Console.WriteLine("2 - Change Debit Comission");
        Console.WriteLine("3 - Change Deposit Interest");
        Console.WriteLine("4 - Change Doubtful Client Limits");
    }

    private void AddClientToBank()
    {
        Console.Clear();
        PrintBanksList();
        ClientBuilder builder = new ();

        builder.SetFullName(InputFullName());
        builder.SetAddress(InputAddress());
        builder.SetPassportData(InputPassportData());
        Client client = builder.CreateClient();

        while (true)
        {
            bool clientAdded = true;
            try
            {
                FindBankByInputID().AddNewClient(client);
            }
            catch (BanksException)
            {
                Console.WriteLine("Client with given PassportData already in the given bank!");
                clientAdded = false;
            }

            if (clientAdded)
            {
                Console.WriteLine("Client was successfully added!");
                return;
            }

            Console.WriteLine("Do you want to enter new PassportData?");
            if (!Confirm())
                return;

            builder.SetPassportData(InputPassportData());
            client = builder.CreateClient();
        }
    }

    private Bank FindBankByInputID()
    {
        while (true)
        {
            Console.WriteLine("Enter banks id");
            Guid bank_id = InputGuid();
            Bank bank;
            try
            {
                bank = _centralBank.GetBankByID(bank_id);
            }
            catch (BanksException)
            {
                Console.WriteLine("Bank with given id doesn't exist!");
                continue;
            }

            return bank;
        }
    }

    private FullName InputFullName()
    {
        string name = InputClientName();
        string surname = InputClientSurname();
        return new FullName(name, surname);
    }

    private string InputClientName()
    {
        string? name;
        while (true)
        {
            Console.WriteLine("Enter client name: ");
            name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
                break;
            Console.WriteLine("Name can not be null or whitespace");
        }

        return name;
    }

    private string InputClientSurname()
    {
        string? surname;
        while (true)
        {
            Console.WriteLine("Enter client surname: ");
            surname = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(surname))
                break;
            Console.WriteLine("Surname can not be null or whitespace");
        }

        return surname;
    }

    private Address? InputAddress()
    {
        Console.WriteLine("Do you want to skip this part (Address will be set to null (No address) ))?");
        if (Confirm())
        {
            return null;
        }

        string country = InputCountryName();
        string city = InputCityName();
        string street = InputStreetName();
        int building = InputBuildingNumber();

        return new Address(country, city, street, building);
    }

    private string InputCountryName()
    {
        string? country;
        while (true)
        {
            Console.WriteLine("Enter client country: ");
            country = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(country))
                break;
            Console.WriteLine("Country can not be null or whitespace");
        }

        return country;
    }

    private string InputCityName()
    {
        string? city;
        while (true)
        {
            Console.WriteLine("Enter client city: ");
            city = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(city))
                break;
            Console.WriteLine("City can not be null or whitespace");
        }

        return city;
    }

    private string InputStreetName()
    {
        string? street;
        while (true)
        {
            Console.WriteLine("Enter client street: ");
            street = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(street))
                break;
            Console.WriteLine("Street can not be null or whitespace");
        }

        return street;
    }

    private int InputBuildingNumber()
    {
        int building;
        while (true)
        {
            const int MinBuildingNumber = 0;
            Console.WriteLine("Enter client building number: ");
            building = Convert.ToInt32(Console.ReadLine());
            if (building > MinBuildingNumber)
                break;
            Console.WriteLine("Building number can not be zero or negative");
        }

        return building;
    }

    private PassportData? InputPassportData()
    {
        Console.WriteLine("Do you want to skip this part(PassportData will be set to null (No PassportData) ))?");
        if (Confirm())
        {
            return null;
        }

        int series = InputPassportSeries();
        int number = InputPassportNumber();
        return new PassportData(number, series);
    }

    private int InputPassportSeries()
    {
        int series;
        while (true)
        {
            const int MinSeries = 1000;
            const int MaxSeries = 9999;
            Console.WriteLine("Enter passport series: ");
            series = Convert.ToInt32(Console.ReadLine());
            if (!(series < MinSeries || series > MaxSeries))
            {
                break;
            }

            Console.WriteLine("Incorrect passport series, try again");
        }

        return series;
    }

    private int InputPassportNumber()
    {
        int number;
        while (true)
        {
            const int MinNumber = 100000;
            const int MaxNumber = 999999;
            Console.WriteLine("Enter passport number: ");
            number = Convert.ToInt32(Console.ReadLine());
            if (!(number < MinNumber || number > MaxNumber))
                break;

            Console.WriteLine("Incorrect passport number, try again");
        }

        return number;
    }

    private void AccountsManipulating()
    {
        Console.Clear();
        Guid account_id = InputGuid();

        Console.WriteLine("Choose one of these options:");
        while (true)
        {
            AccountsManipulatingHelp();

            int num = Convert.ToInt32(Console.ReadLine());
            if (num == 0)
            {
                return;
            }
            else if (num == 1)
            {
                double amount = InputMoneyAmount();
                _centralBank.ReplenishAccount(account_id, amount);
            }
            else if (num == 2)
            {
                try
                {
                    double amount = InputMoneyAmount();
                    _centralBank.WithdrawFromAccount(account_id, amount);
                }
                catch (BanksException)
                {
                    Console.WriteLine($"Account with id: {account_id} does not enough money or doesn't exists!");
                }
            }
            else if (num == 2)
            {
                Guid account_transfer_id;
                while (true)
                {
                    account_transfer_id = InputGuid();
                    if (account_id != account_transfer_id)
                        break;
                    Console.WriteLine("Transfer account can not be same with the chosen one, please select another one");
                }

                double amount = InputMoneyAmount();

                try
                {
                    _centralBank.TransferMoneyBetweenAccounts(account_id, account_transfer_id, amount);
                }
                catch (BanksException)
                {
                    Console.WriteLine("Account doesn't have enought money, or one of given accounts doesn't exist!");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Incorrect input! Plese use on of these options:");
            }
        }
    }

    private void AccountsManipulatingHelp()
    {
        Console.WriteLine("0 - Go back");
        Console.WriteLine("1 - Replenish to account");
        Console.WriteLine("2 - Withdraw from account");
        Console.WriteLine("3 - Transfer from this account to another");
    }

    private Guid InputGuid()
    {
        while (true)
        {
            Console.WriteLine("Enter id: ");

            string? id = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("id can not be null or white space!");
                continue;
            }

            try
            {
                return Guid.Parse(id);
            }
            catch (Exception)
            {
                Console.WriteLine("Incorrect id! Try again");
            }
        }
    }

    private double InputMoneyAmount()
    {
        const double MinMoneyAmount = 0;

        double amount;
        while (true)
        {
            Console.WriteLine("Enter money amount: ");
            amount = Convert.ToDouble(Console.ReadLine());
            if (amount > MinMoneyAmount)
                break;

            Console.WriteLine("Incorrect money amount, try again");
        }

        return amount;
    }

    private void CancelTransaction()
    {
        Console.Clear();
        while (true)
        {
            Console.WriteLine("Enter transaction id");
            Guid transaction_id = InputGuid();
            try
            {
                _centralBank.CancelTransaction(transaction_id);
            }
            catch (BanksException)
            {
                Console.WriteLine($"Transaction with id {transaction_id} doesn't exist or has been already canceled!");
                continue;
            }

            break;
        }
    }

    private void PrintBanksList()
    {
        Console.Clear();
        Console.WriteLine("Banks List:");
        foreach (Bank bank in _centralBank.Banks)
        {
            PrintBankInfo(bank);
        }
    }

    private void PrintClientsList()
    {
        Console.Clear();
        Console.WriteLine("Clients List:");
        foreach (Bank bank in _centralBank.Banks)
        {
            foreach (IClient client in bank.Clients)
            {
                PrintClientInfo(client);
            }
        }
    }

    private void PrintAccountsList()
    {
        Console.Clear();
        Console.WriteLine("Accounts List:");
        foreach (Bank bank in _centralBank.Banks)
        {
            foreach (IClient client in bank.Clients)
            {
                foreach (IAccount account in client.Accounts)
                {
                    PrintAccountInfo(account);
                }
            }
        }
    }

    private void PrintTransactionsList()
    {
        Console.Clear();
        Console.WriteLine("Transactions List:");
        foreach (ITransaction transaction in _centralBank.Transactions)
        {
            PrintTransactionInfo(transaction);
        }
    }

    private void PrintTransactionInfo(ITransaction transaction)
    {
        Console.WriteLine("------------------------------------------------------------------------------------------------------");
        Console.WriteLine($"Transaction type: {transaction.GetType().ToString()}");
        Console.WriteLine($"Transaction Id: {transaction.Id}");
        Console.WriteLine($"Transaction Account: {transaction.Account}");
        Console.WriteLine($"Transaction Money Amount: {transaction.MoneyAmount}");
        Console.WriteLine($"Transaction Comission: {transaction.Comission}");
        Console.WriteLine($"Transaction Date: {transaction.DateTime}");
        Console.WriteLine($"Transaction Canceled: {transaction.Canceled}");
        Console.WriteLine("------------------------------------------------------------------------------------------------------");
        Console.WriteLine();
    }

    private void PrintBankInfo(Bank bank)
    {
        Console.WriteLine("------------------------------------------------------------------------------------------------------");
        Console.WriteLine($"Bank Name: {bank.Name}");
        Console.WriteLine($"Bank Id: {bank.Id}");
        Console.WriteLine($"Bank CreditComission: {bank.CreditComission}");
        Console.WriteLine($"Bank DebitComission: {bank.DebitComission}");
        Console.WriteLine($"Bank DepositInterest: {bank.DepositInterest}");
        Console.WriteLine($"Bank DoubtfulClientLimit: {bank.DoubtfulClientLimit}");
        Console.WriteLine("------------------------------------------------------------------------------------------------------");
        Console.WriteLine();
    }

    private void PrintClientInfo(IClient client)
    {
        Console.WriteLine("------------------------------------------------------------------------------------------------------");
        Console.WriteLine($"Client Name: {client.FullName.Name}");
        Console.WriteLine($"Client Surname: {client.FullName.Surname}");
        Console.WriteLine($"Client Id: {client.Id}");
        Console.WriteLine($"Client PassportData: {client.PassportData?.ToString()}");
        Console.WriteLine($"Client Address: {client.Address?.ToString()}");
        Console.WriteLine($"Client ChangesSubscription: {client.ChangesSubscription}");
        Console.WriteLine($"Client ConfirmedStatus: {client.ConfirmedStatus}");
        Console.WriteLine("------------------------------------------------------------------------------------------------------");
        Console.WriteLine();
    }

    private void PrintAccountInfo(IAccount account)
    {
        Console.WriteLine("------------------------------------------------------------------------------------------------------");
        Console.WriteLine($"Account type: {account.GetType().ToString()}");
        Console.WriteLine($"Account Id: {account.Id}");
        Console.WriteLine($"Account Comission: {account.Comission}");
        Console.WriteLine($"Account Client Id: {account.Client.Id}");
        Console.WriteLine($"Account Money: {account.Money}");
        Console.WriteLine("------------------------------------------------------------------------------------------------------");
        Console.WriteLine();
    }

    private void ControlConsoleHelp()
    {
        Console.WriteLine("0 - Go back");
        Console.WriteLine("1 - Add banks");
        Console.WriteLine("2 - Change banks settings");
        Console.WriteLine("3 - Add client to bank");
        Console.WriteLine("4 - Manipulate account");
        Console.WriteLine("5 - Transaction canceling");
        Console.WriteLine("6 - Print list of banks");
        Console.WriteLine("7 - Print list of clients");
        Console.WriteLine("8 - Print list of accounts");
        Console.WriteLine("9 - Print list of transactions");
    }
}