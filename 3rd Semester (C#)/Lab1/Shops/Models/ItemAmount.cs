using Shops.Entities;

namespace Shops.Models
{
    public class ItemAmount
    {
        public ItemAmount(Item price, uint amount)
        {
            Item = price;
            Amount = amount;
        }

        public Item Item { get; }
        public uint Amount { get; }
    }
}
