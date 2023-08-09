using Shops.Entities;
using Shops.Interfaces;
using Shops.Models;

namespace Shops.Services;

public interface IManager
{
    Customer AddCustomer(ICashAccount account, string name);
    Shop AddShop(string name, IAdress adress, Dictionary<IItem, PriceAmount> products, double money_amount, double extra_charge);
    Shop FindBestShop(List<ItemAmount> products);
    Shop FindBestShop(List<ItemAmount> products, List<Shop> shops);
    Customer? FindCustomer(Guid id);
    Shop? FindShop(Guid id);
    Customer GetCustomer(Guid id);
    IReadOnlyList<Customer>? GetCustomers();
    Shop GetShop(Guid id);
    IReadOnlyList<Shop>? GetShops();
}