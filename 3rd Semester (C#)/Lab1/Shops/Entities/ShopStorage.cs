using Shops.Exceptions;
using Shops.Interfaces;
using Shops.Models;

namespace Shops.Entities
{
    public class ShopStorage : IShopStorage
    {
        private const double MinExtraCharge = 1;
        private Dictionary<IItem, PriceAmount> _products = new ();

        public ShopStorage()
            : this(new Dictionary<IItem, PriceAmount>())
        { }

        public ShopStorage(Dictionary<IItem, PriceAmount> products)
        {
            if (products is null)
            {
                _products = new Dictionary<IItem, PriceAmount>();
            }
            else
            {
                _products = products;
            }
        }

        public IReadOnlyDictionary<IItem, PriceAmount> Products => _products;

        public void AddItems(Dictionary<IItem, PriceAmount> products, double extra_charge)
        {
            if (products is null)
            {
                throw new ShopStorageProductsNullReferenceException($"Failed to AddItems to storage {this}, products can not be null");
            }

            if (extra_charge <= MinExtraCharge)
            {
                throw new ShopStorageExtraChargeInvalidArgumentException($"Failed to AddItems to storage {this}, extra_charge can not be <= {MinExtraCharge}");
            }

            foreach (KeyValuePair<IItem, PriceAmount> product in products)
            {
                if (_products.ContainsKey(product.Key))
                {
                    _products[product.Key].Amount += product.Value.Amount;
                    _products[product.Key].Price += product.Value.Price * product.Value.Amount;
                }
                else
                {
                    _products.Add(product.Key, product.Value);
                }
            }
        }

        public void RemoveItems(List<ItemAmount> products)
        {
            products.ForEach(product => _products[product.Item].Amount -= product.Amount);
        }
    }
}
