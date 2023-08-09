using DataAccessLayer.MessageSources;

namespace DataAccessLayer.Messages;

public enum MessageState
{
    New,
    Recieved,
    Processed,
}

public abstract class AbstractMessage
{
    public Guid ID { get; } = Guid.NewGuid();
    public string? Answer { get; private set; }
    internal abstract IMessageSource Source { get; }
    internal MessageState State { get; private set; } = MessageState.New;
    internal abstract string Text { get; }
    internal abstract uint AccessLevel { get; }

    internal void SetAnswer(string answer)
    {
        if (string.IsNullOrWhiteSpace(answer))
            throw new DalException("Failed to SetAnswer. Given value answer can not be null or white space");
        Answer = answer;
        SetStateToProcessed();
    }

    internal void SetStateToProcessed()
    {
        State = MessageState.Processed;
    }

    internal void SetStateToRecieved()
    {
        State = MessageState.Recieved;
    }
}
