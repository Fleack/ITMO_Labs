using Shops.Exceptions;
using Shops.Interfaces;

namespace Shops.Entities
{
    public class Item : IItem
    {
        public Item(string name, string manufacturer)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ItemNameNullOrWhiteSpaceException("Failed to construct Item, name can not be null or empty");
            }

            if (string.IsNullOrWhiteSpace(manufacturer))
            {
                throw new ItemManufacturerNullOrWhiteSpaceException("Failed to construct Item, manufacturer can not be null or empty");
            }

            Name = name;
            Manufacturer = manufacturer;
        }

        public string Name { get; }
        public string Manufacturer { get; }
    }
}
