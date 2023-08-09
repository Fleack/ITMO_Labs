namespace DataAccessLayer.Entities;

public class Boss : AbstractEmployee
{
    public Boss(string login, string password, uint accessLevel, List<Employee> subordinates)
    {
        if (string.IsNullOrWhiteSpace(login))
            throw new DalException($"Failed to construct Employee. Given value login {login} can not be null or whitespace");
        if (string.IsNullOrWhiteSpace(password))
            throw new DalException($"Failed to construct Employee. Given value password {password} can not be null or whitespace");

        Subordinates = subordinates;
        Subordinates ??= new ();
        Login = login;
        Password = password;
        AccessLevel = accessLevel;
    }

    internal override string Login { get; }
    internal override string Password { get; }
    internal override uint AccessLevel { get; }
    internal uint ReportsCount { get; private set; }

    public void IncreaseReportsCount()
    {
        ReportsCount++;
    }

    public IReadOnlyList<Employee> GetSubordinates()
    {
        return Subordinates;
    }

    internal override void ResetProgress()
    {
        ReportsCount = 0;
    }
}
