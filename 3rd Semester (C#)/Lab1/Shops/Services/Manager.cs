using Shops.Entities;
using Shops.Exceptions;
using Shops.Interfaces;
using Shops.Models;

namespace Shops.Services;

public class Manager : IManager
{
    private readonly List<Shop> _shops = new List<Shop>();
    private readonly List<Customer> _customers = new List<Customer>();

    public Shop AddShop(string name, IAdress adress, Dictionary<IItem, PriceAmount> products, double money_amount, double extra_charge)
    {
        var new_shop = new Shop(name, adress, products, money_amount, extra_charge);
        _shops.Add(new_shop);
        return new_shop;
    }

    public Customer AddCustomer(ICashAccount account, string name)
    {
        Customer newCustomer = new (account, name);
        _customers.Add(newCustomer);
        return newCustomer;
    }

    public Customer GetCustomer(Guid id)
    {
        Customer? customer = _customers.SingleOrDefault(customer => customer.ID == id);
        if (customer is null)
        {
            throw new FailedToGetCustomerException($"Failed to get customer: {customer}. This customer does not exist");
        }

        return customer;
    }

    public Customer? FindCustomer(Guid id)
    {
        Customer customer = _customers.First(customer => customer.ID == id);
        return customer;
    }

    public Shop? FindShop(Guid id)
    {
        Shop customer = _shops.First(shop => shop.ID == id);
        return customer;
    }

    public Shop GetShop(Guid id)
    {
        Shop? shop = _shops.SingleOrDefault(shop => shop.ID == id);
        if (shop is null)
        {
            throw new FailedToGetShopException($"Failed to get customer: {shop}. This customer does not exist");
        }

        return shop;
    }

    public IReadOnlyList<Customer>? GetCustomers()
    {
        return _customers;
    }

    public IReadOnlyList<Shop>? GetShops()
    {
        return _shops;
    }

    public Shop FindBestShop(List<ItemAmount> products)
    {
        return FindBestShop(products, _shops);
    }

    public Shop FindBestShop(List<ItemAmount> products, List<Shop> shops)
    {
        double min_price = double.PositiveInfinity;
        Shop? best_shop = null;
        foreach (Shop shop in shops)
        {
            double cur_price = 0;
            IReadOnlyDictionary<IItem, PriceAmount> cur_shop_products = shop.GetDictionaryOfProducts();
            foreach (ItemAmount product in products)
            {
                if (!cur_shop_products.ContainsKey(product.Item))
                {
                    cur_price = double.PositiveInfinity;
                    break;
                }

                if (cur_shop_products[product.Item].Amount < product.Amount)
                {
                    cur_price = double.PositiveInfinity;
                    break;
                }

                cur_price += cur_shop_products[product.Item].Price * product.Amount;
            }

            if (cur_price < min_price)
            {
                min_price = cur_price;
                best_shop = shop;
            }
        }

        if (best_shop is null)
        {
            throw new BestShopNullReferenceException($"Failed to find best shop. Shop that has all products in {products} does not exist!");
        }

        return best_shop;
    }
}