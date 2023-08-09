namespace DataAccessLayer.Entities;

public abstract class AbstractEmployee
{
    public Guid ID { get; } = Guid.NewGuid();
    internal abstract string Login { get; }
    internal abstract string Password { get; }
    internal abstract uint AccessLevel { get; }
    protected List<Employee> Subordinates { get; set; } = new ();

    public void AddSubordinate(Employee new_employee)
    {
        if (new_employee is null)
            throw new DalException("Failed to AddSubordinate. Given value new_employee can not be null");
        Subordinates.Add(new_employee);
    }

    public void RemoveSubordinate(Employee employee)
    {
        if (employee is null)
            throw new DalException("Failed to RemoveSubordinate. Given value employee can not be null");
        Subordinates.Remove(employee);
    }

    internal abstract void ResetProgress();
}
