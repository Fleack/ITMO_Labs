using Banks.Entities;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Tools;

public class ClientBuilder : IClientBuilder
{
    private Address? _clientAddress = null;
    private PassportData? _clientPassportData = null;
    private FullName? _clientFullName;

    public void SetAddress(Address? address)
    {
        _clientAddress = address;
    }

    public void SetPassportData(PassportData? passportData)
    {
        _clientPassportData = passportData;
    }

    public void SetFullName(FullName fullName)
    {
        if (fullName is null)
        {
            throw new BanksException("Can not SetFullName, fullName can not be null!");
        }

        _clientFullName = fullName;
    }

    public Client CreateClient()
    {
        if (_clientFullName is null)
        {
            throw new BanksException("Can not create client, client has to have a name!");
        }

        Client client = new (_clientFullName, _clientAddress, _clientPassportData);
        return client;
    }
}
