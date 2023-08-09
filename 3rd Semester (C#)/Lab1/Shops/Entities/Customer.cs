using Shops.Exceptions;
using Shops.Interfaces;

namespace Shops.Entities
{
    public class Customer : ICustomer
    {
        public Customer(ICashAccount account, string name)
        {
            if (account is null)
            {
                throw new ICashAccountNullReferenceException("Failed to construct customer, account can not be null");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new CustomerNameNullOrWhiteSpaceException("Failed to construct customer, name can not be null or empty");
            }

            Account = account;
            ID = Guid.NewGuid();
            Name = name;
        }

        public ICashAccount Account { get; }
        public Guid ID { get; }
        public string Name { get; }
    }
}
