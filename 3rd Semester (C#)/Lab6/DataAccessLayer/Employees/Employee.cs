namespace DataAccessLayer.Entities;

public class Employee : AbstractEmployee
{
    public Employee(string login, string password, uint accessLevel, List<Employee> subordinates, AbstractEmployee head)
    {
        if (head is null)
            throw new DalException("Failed to construct Employee. Given value head can not be null");
        if (string.IsNullOrWhiteSpace(login))
            throw new DalException($"Failed to construct Employee. Given value login {login} can not be null or whitespace");
        if (string.IsNullOrWhiteSpace(password))
            throw new DalException($"Failed to construct Employee. Given value password {password} can not be null or whitespace");

        Subordinates = subordinates;
        Subordinates ??= new ();
        Head = head;
        Login = login;
        Password = password;
        ProcessedMessagesCount = 0;
        AccessLevel = accessLevel;
    }

    public uint ProcessedMessagesCount { get; private set; }
    internal override string Login { get; }
    internal override string Password { get; }
    internal override uint AccessLevel { get; }
    internal AbstractEmployee Head { get; private set; }

    public void IncreaseMessagesCount()
    {
        ProcessedMessagesCount++;
    }

    internal override void ResetProgress()
    {
        ProcessedMessagesCount = 0;
    }

    internal void SetHead(AbstractEmployee new_head)
    {
        if (new_head is null)
            throw new DalException("Failed to SetHead. Given value new_head can not be null");
        Head = Head;
    }
}
