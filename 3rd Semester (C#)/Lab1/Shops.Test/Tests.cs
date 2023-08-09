using Shops.Entities;
using Shops.Interfaces;
using Shops.Models;
using Shops.Services;
using Xunit;

namespace Shops.Test
{
    public class Tests
    {
        [Fact]
        public void ShopProductsDelivery()
        {
            var shop_adress = new Adress("Rosiya", "Piter", "Kronv", 99);
            Shop shop = new ("Shop", shop_adress, new Dictionary<IItem, PriceAmount>(), 9999999, 1.2);

            Item item1 = new ("item1", "manufacturer1");
            Item item2 = new ("item2", "manufacturer2");

            PriceAmount pa1 = new (100, 3);
            PriceAmount pa2 = new (50, 100);

            Dictionary<IItem, PriceAmount> provider_list = new ();
            CashAccount provider_account = new (300);

            provider_list.Add(item1, pa1);
            provider_list.Add(item2, pa2);

            shop.OrderProducts(provider_list, provider_account);

            Assert.Contains(item1, shop.GetDictionaryOfProducts().Keys);
            Assert.Contains(item2, shop.GetDictionaryOfProducts().Keys);
        }

        [Fact]
        public void ChangeProductPrice()
        {
            const double shop_money = 50000;
            const double multiplier = 1.2;
            const double new_price = 2000000;
            const double item1_price = 100;
            const double item2_price = 50;
            const uint item1_count = 3;
            const uint item2_count = 3;

            Item item1 = new ("item1", "manufacturer1");
            PriceAmount pa1 = new (item1_price, item1_count);
            Item item2 = new ("item2", "manufacturer2");
            PriceAmount pa2 = new (item2_price, item2_count);

            Dictionary<IItem, PriceAmount> products = new ();

            products.Add(item1, pa1);
            products.Add(item2, pa2);

            var shop_adress = new Adress("Rosiya", "Piter", "Kronv", 99);
            Shop shop = new ("Shop", shop_adress, products, shop_money, multiplier);

            shop.SetProductPrice(item1, new_price);

            Assert.Equal(new_price, shop.GetDictionaryOfProducts()[item1].Price);
        }

        [Fact]
        public void BuyListOfProducts()
        {
            const double shop_money = 50000;
            const double customer_money = 2000;
            const double multiplier = 1.2;
            const double item1_price = 100;
            const double item2_price = 50;
            const double item3_price = 15;
            const uint item1_count = 3;
            const uint item2_count = 100;
            const uint item3_count = 15;
            const uint item1_customer_count = 3;
            const uint item3_customer_count = 9;

            Item item1 = new ("item1", "manufacturer1");
            PriceAmount pa1 = new (item1_price, item1_count);
            Item item2 = new ("item2", "manufacturer2");
            PriceAmount pa2 = new (item2_price, item2_count);
            Item item3 = new ("item3", "manufacturer3");
            PriceAmount pa3 = new (item3_price, item3_count);

            CashAccount cashAccount = new (customer_money);
            Customer customer = new (cashAccount, "Boba");

            Dictionary<IItem, PriceAmount> products = new ();

            products.Add(item1, pa1);
            products.Add(item2, pa2);
            products.Add(item3, pa3);

            var shop_adress = new Adress("Rosiya", "Piter", "Kronv", 99);
            Shop shop = new ("Shop", shop_adress, products, shop_money, multiplier);

            List<ItemAmount> buy_list = new ();
            buy_list.Add(new (item1, item1_customer_count));
            buy_list.Add(new (item3, item3_customer_count));

            shop.CustomerBuysProducts(customer, buy_list);
            double products_sum = (item1_customer_count * item1_price) + (item3_customer_count * item3_price);

            Assert.Equal(shop_money + products_sum, shop.Account.Money);
            Assert.Equal(customer_money - products_sum, customer.Account.Money);
            Assert.Equal(item1_count - item1_customer_count, shop.GetDictionaryOfProducts()[item1].Amount);
            Assert.Equal(item3_count - item3_customer_count, shop.GetDictionaryOfProducts()[item3].Amount);
        }

        [Fact]
        public void FindingShopWithBestPrice()
        {
            const double shop1_money = 50000;
            const double shop2_money = 25000;
            const double multiplier = 1.2;
            const double item1_price_shop1 = 100;
            const double item2_price_shop1 = 50;
            const double item1_price_shop2 = 120;
            const double item2_price_shop2 = 40;
            const uint item1_count_shop1 = 3;
            const uint item2_count_shop1 = 100;
            const uint item1_count_shop2 = 5;
            const uint item2_count_shop2 = 80;
            const uint item1_customer_count = 3;
            const uint item2_customer_count = 15;
            Item item1 = new ("item1", "manufacturer1");
            Item item2 = new ("item2", "manufacturer2");
            PriceAmount pa2_shop1 = new (item2_price_shop1, item2_count_shop1);
            PriceAmount pa1_shop1 = new (item1_price_shop1, item1_count_shop1);
            PriceAmount pa2_shop2 = new (item2_price_shop2, item2_count_shop2);
            PriceAmount pa1_shop2 = new (item1_price_shop2, item1_count_shop2);

            Dictionary<IItem, PriceAmount> products_shop1 = new ();
            products_shop1.Add(item1, pa1_shop1);
            products_shop1.Add(item2, pa2_shop1);
            var shop1_adress = new Adress("Rosiya", "Piter", "Kronv", 99);
            Shop shop1 = new ("Shop1", shop1_adress, products_shop1, shop1_money, multiplier);

            Dictionary<IItem, PriceAmount> products_shop2 = new ();
            products_shop2.Add(item1, pa1_shop2);
            products_shop2.Add(item2, pa2_shop2);
            var shop2_adress = new Adress("Rosiya", "Piter", "Kronv", 98);
            Shop shop2 = new ("Shop2", shop2_adress, products_shop2, shop2_money, multiplier);

            List<ItemAmount> buy_list = new ();
            buy_list.Add(new (item1, item1_customer_count));
            buy_list.Add(new (item2, item2_customer_count));

            List<Shop> shop_list = new ();
            shop_list.Add(shop1);
            shop_list.Add(shop2);
            Manager shopFinder = new ();
            Shop best_shop = shopFinder.FindBestShop(buy_list, shop_list);

            Assert.Equal(best_shop, shop2);
        }
    }
}
