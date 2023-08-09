using DataAccessLayer.Messages;

namespace DataAccessLayer.MessageSources;

public interface IMessageSource
{
    string Name { get; }

    AbstractMessage SendMessage(string message, uint accessLevel);
}
