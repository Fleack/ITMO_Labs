using DataAccessLayer.Messages;

namespace DataAccessLayer.MessageSources;

public class MessagerMessageSource : IMessageSource
{
    public MessagerMessageSource(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DalException($"Failed to construct MessagerMessageSource. Given value name {name} is incorrect");
        Name = name;
    }

    public string Name { get; }

    public AbstractMessage SendMessage(string message, uint accessLevel)
    {
        return new MessagerMessage(this, message, accessLevel);
    }
}