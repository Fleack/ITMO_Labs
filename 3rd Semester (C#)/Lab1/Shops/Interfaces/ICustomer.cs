namespace Shops.Interfaces
{
    public interface ICustomer
    {
        ICashAccount Account { get; }
        Guid ID { get; }
        string Name { get; }
    }
}
