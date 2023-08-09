using Shops.Exceptions;
using Shops.Interfaces;
using Shops.Models;

namespace Shops.Entities
{
    public class Shop : IShop
    {
        private const double MinPrice = 0;
        private const double MinExtraCharge = 1;
        private ShopStorage _storage;
        private double _extra_charge;

        public Shop(string name, IAdress adress, Dictionary<IItem, PriceAmount> products, double money_amount, double extra_charge)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ShopNameNullOrWhiteSpaceException("Failed to construct Shop, name can not be null or empty");
            }

            if (adress is null)
            {
                throw new ShopAdressNullReferenceException("Failed to construct Shop, adress can not be null");
            }

            if (extra_charge <= MinExtraCharge)
            {
                throw new ShopExtraChargeInvalidArgumentException($"Failed to construct Shop, extra_charge can not be <= {MinExtraCharge}");
            }

            if (products is null)
            {
                _storage = new (new Dictionary<IItem, PriceAmount>());
            }
            else
            {
                _storage = new (products);
            }

            Adress = adress;
            Name = name;
            ID = Guid.NewGuid();
            _extra_charge = extra_charge;
            Account = new (money_amount);
        }

        public CashAccount Account { get; }
        public IAdress Adress { get; }
        public string Name { get;  }
        public Guid ID { get; }

        public IReadOnlyDictionary<IItem, PriceAmount> GetDictionaryOfProducts()
        {
            return _storage.Products;
        }

        public void OrderProducts(Dictionary<IItem, PriceAmount> products, ICashAccount provider)
        {
            if (provider is null)
            {
                throw new ShopProvideNullReferenceException("Failed to OrderProducts, provider can not be null");
            }

            if (products is null)
            {
                throw new ShopProductsNullReferenceException("Failed to OrderProducts, products can not be null");
            }

            double sum = 0;
            products.ToList().ForEach(product => sum += product.Value.Price * product.Value.Amount);
            if (sum > Account.Money)
            {
                throw new ShopNotEnoughMoneyException($"Failed to OrderProducts, Shop: {this} does not have enough money");
            }

            Account.SendMoneyTo(provider, sum);
            _storage.AddItems(products, _extra_charge);
        }

        public void SetProductPrice(IItem item, double new_price)
        {
            if (item is null)
            {
                throw new ShopItemNullReferenceException("Failed to SetProductPrice, item can not be null");
            }

            if (new_price <= MinPrice)
            {
                throw new ShopNewPriceInvalidArgumentException($"Failed to OrderProducts, new_price: {new_price} can not be 0 or negative");
            }

            if (!_storage.Products.ContainsKey(item))
            {
                throw new ShopDoesNotContainProductException($"Failed to OrderProducts, Storage: {_storage} does not contain given item: {item}");
            }

            _storage.Products[item].Price = new_price;
        }

        public void CustomerBuysProducts(ICustomer customer, List<ItemAmount> products)
        {
            if (customer is null)
            {
                throw new ShopCustomerNullReferenceException("Failed to CustomerBuysProducts, customer can not be null");
            }

            if (products is null)
            {
                throw new ShopProductsNullReferenceException("Failed to CustomerBuysProducts, products can not be null");
            }

            double sum = 0;
            products.ForEach(product => sum += _storage.Products[product.Item].Price * product.Amount);
            if (sum > customer.Account.Money)
            {
                throw new ShopCustomerMoneyException($"Failed to CustomerBuysProducts, customer: {customer} does not have enough money");
            }

            foreach (ItemAmount product in products)
            {
                if (!_storage.Products.ContainsKey(product.Item))
                {
                    throw new ShopDoesNotContainProductException($"Failed to CustomerBuysProducts, storage: {_storage} does not contain item: {product.Item}");
                }

                if (_storage.Products[product.Item].Amount < product.Amount)
                {
                    throw new ShopNotEnoughProductsException($"Failed to CustomerBuysProducts, storage: {_storage} does not contain {product.Amount} items: {product.Item}");
                }
            }

            customer.Account.SendMoneyTo(Account, sum);
            _storage.RemoveItems(products);
        }
    }
}
