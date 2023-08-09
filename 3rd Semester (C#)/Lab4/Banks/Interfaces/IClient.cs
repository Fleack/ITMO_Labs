using Banks.Models;

namespace Banks.Interfaces;

public interface IClient
{
    Guid Id { get; }
    FullName FullName { get; }
    PassportData? PassportData { get; }
    Address? Address { get; }
    bool ConfirmedStatus { get; }
    bool ChangesSubscription { get; }
    IReadOnlyList<IAccount> Accounts { get; }

    void AccountRulesChanged();
    void AddNewAccount(IAccount account);
}
