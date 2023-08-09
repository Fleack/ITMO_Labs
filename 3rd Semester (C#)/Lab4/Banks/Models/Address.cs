using Banks.Tools;

namespace Banks.Models;

public class Address
{
    public Address(string country, string city, string street, int buildingNumber)
    {
        if (string.IsNullOrWhiteSpace(country))
        {
            throw new BanksException("Failed to construct Adress, country can not be null or empty");
        }

        if (string.IsNullOrWhiteSpace(city))
        {
            throw new BanksException("Failed to construct Adress, city can not be null or empty");
        }

        if (string.IsNullOrWhiteSpace(street))
        {
            throw new BanksException("Failed to construct Adress, street can not be null or empty");
        }

        Country = country;
        City = city;
        Street = street;
        BuildingNumber = buildingNumber;
    }

    public string Country { get; }
    public string City { get; }
    public string Street { get; }
    public int BuildingNumber { get; }
}
