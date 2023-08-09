using Banks.Models;

namespace Banks.Interfaces;

public interface IClientBuilder
{
    void SetPassportData(PassportData passportData);
    void SetAddress(Address address);
}
