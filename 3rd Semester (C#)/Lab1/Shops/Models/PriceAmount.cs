namespace Shops.Models
{
    public class PriceAmount
    {
        public PriceAmount(double price, uint amount)
        {
            Price = price;
            Amount = amount;
        }

        public double Price { get; set; }
        public uint Amount { get; set; }
    }
}
