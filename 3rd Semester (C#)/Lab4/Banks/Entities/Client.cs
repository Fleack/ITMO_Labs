using Banks.Interfaces;
using Banks.Models;
using Banks.Tools;

namespace Banks.Entities;

public class Client : IClient
{
    private List<IAccount> _accounts = new List<IAccount>();
    private Address? _address;
    private PassportData? _passportData;

    public Client(FullName fullName, Address? address, PassportData? passportData)
    {
        if (fullName is null)
        {
            throw new BanksException($"Failed to construct Client, given value: fullName {fullName} can not be null");
        }

        FullName = fullName;
        Address = address;
        PassportData = passportData;
        ChangesSubscription = false;
        Id = Guid.NewGuid();
    }

    public IReadOnlyList<IAccount> Accounts => _accounts;
    public Guid Id { get; }
    public FullName FullName { get; }
    public bool ConfirmedStatus { get; private set; }
    public bool ChangesSubscription { get; private set; }
    public bool AreRulesChanged { get; private set; }

    public PassportData? PassportData
    {
        get
        {
            return _passportData;
        }
        set
        {
            _passportData = value;
            SetStatus();
        }
    }

    public Address? Address
    {
        get
        {
            return _address;
        }
        set
        {
            _address = value;
            SetStatus();
        }
    }

    public void AddNewAccount(IAccount account)
    {
        _accounts.Add(account);
    }

    public void SubscribeToChanges()
    {
        ChangesSubscription = true;
    }

    public void UnsubscribeToChanges()
    {
        ChangesSubscription = false;
    }

    public void AccountRulesChanged()
    {
        AreRulesChanged = true;
    }

    private void SetStatus()
    {
        ConfirmedStatus = _address != null && _passportData != null;
    }
}
