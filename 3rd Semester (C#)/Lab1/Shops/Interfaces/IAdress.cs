namespace Shops.Interfaces
{
    public interface IAdress
    {
        string Country { get; }
        string City { get; }
        string Street { get; }
        uint BuildingNumber { get; }
    }
}
