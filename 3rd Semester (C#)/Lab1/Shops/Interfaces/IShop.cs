using Shops.Models;

namespace Shops.Interfaces
{
    public interface IShop
    {
        CashAccount Account { get; }
        IAdress Adress { get; }
        string Name { get; }
        Guid ID { get; }

        IReadOnlyDictionary<IItem, PriceAmount> GetDictionaryOfProducts();

        void OrderProducts(Dictionary<IItem, PriceAmount> products, ICashAccount provider);

        void CustomerBuysProducts(ICustomer customer, List<ItemAmount> products);
    }
}
