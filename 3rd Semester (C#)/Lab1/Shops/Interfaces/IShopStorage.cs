using Shops.Interfaces;
using Shops.Models;

namespace Shops.Entities
{
    public interface IShopStorage
    {
        IReadOnlyDictionary<IItem, PriceAmount> Products { get; }

        void AddItems(Dictionary<IItem, PriceAmount> products, double extra_charge);

        void RemoveItems(List<ItemAmount> products);
    }
}