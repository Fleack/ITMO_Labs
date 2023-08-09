using DataAccessLayer.Messages;

namespace DataAccessLayer.MessageSources;

public class EmailMessageSource : IMessageSource
{
    public EmailMessageSource(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DalException($"Failed to construct EmailMessageSource. Given value name {name} is incorrect");
        Name = name;
    }

    public string Name { get; }

    public AbstractMessage SendMessage(string message, uint accessLevel)
    {
        return new EmailMessage(this, message, accessLevel);
    }
}