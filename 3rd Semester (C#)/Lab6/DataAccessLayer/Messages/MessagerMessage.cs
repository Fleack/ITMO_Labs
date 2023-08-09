using DataAccessLayer.MessageSources;

namespace DataAccessLayer.Messages;

public class MessagerMessage : AbstractMessage
{
    internal MessagerMessage(MessagerMessageSource source, string text, uint accessLevel)
    {
        if (source is null)
            throw new DalException("Failed to construct EmailMessage. Given value source can not be null");
        if (string.IsNullOrWhiteSpace(text))
            throw new DalException("Failed to construct EmailMessage. Given value text can not be null or white space");

        Source = source;
        Text = text;
        AccessLevel = accessLevel;
    }

    internal override IMessageSource Source { get; }
    internal override string Text { get; }
    internal override uint AccessLevel { get; }
}
