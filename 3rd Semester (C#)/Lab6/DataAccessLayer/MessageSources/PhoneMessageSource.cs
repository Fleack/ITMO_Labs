using DataAccessLayer.Messages;

namespace DataAccessLayer.MessageSources;

public class PhoneMessageSource : IMessageSource
{
    public PhoneMessageSource(string number)
    {
        if (string.IsNullOrWhiteSpace(number) || number.Any(c => !char.IsDigit(c)))
            throw new DalException($"Failed to construct PhoneMessageSource. Given value number {number} is incorrect");
        Name = number;
    }

    public string Name { get; }

    public AbstractMessage SendMessage(string message, uint accessLevel)
    {
        return new PhoneMessage(this, message, accessLevel);
    }
}
