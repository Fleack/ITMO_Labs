using Shops.Exceptions;
using Shops.Interfaces;

namespace Shops.Models
{
    public class Adress : IAdress
    {
        private const uint IncorrectBuildingNumber = 0;

        public Adress(string country, string city, string street, uint buildingNumber)
        {
            if (string.IsNullOrWhiteSpace(country))
            {
                throw new AdressConstructionIncorrectArgumentException("Failed to construct Adress, country can not be null or empty");
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                throw new AdressConstructionIncorrectArgumentException("Failed to construct Adress, city can not be null or empty");
            }

            if (string.IsNullOrWhiteSpace(street))
            {
                throw new AdressConstructionIncorrectArgumentException("Failed to construct Adress, street can not be null or empty");
            }

            if (buildingNumber is IncorrectBuildingNumber)
            {
                throw new AdressConstructionIncorrectArgumentException("Failed to construct Adress, buildingNumber can not be 0");
            }

            if (country.Any(x => char.IsDigit(x)))
            {
                throw new AdressConstructionIncorrectArgumentException("Failed to construct Adress, contry can not contain any digits");
            }

            if (city.Any(x => char.IsDigit(x)))
            {
                throw new AdressConstructionIncorrectArgumentException("Failed to construct Adress, city can not contain any digits");
            }

            if (street.Any(x => char.IsDigit(x)))
            {
                throw new AdressConstructionIncorrectArgumentException("Failed to construct Adress, street can not contain any digits");
            }

            Country = country;
            City = city;
            Street = street;
            BuildingNumber = buildingNumber;
        }

        public string Country { get; }
        public string City { get; }
        public string Street { get; }
        public uint BuildingNumber { get; }
    }
}
