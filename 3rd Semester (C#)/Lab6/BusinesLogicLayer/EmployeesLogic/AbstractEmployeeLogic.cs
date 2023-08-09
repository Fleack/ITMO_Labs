using DataAccessLayer.Manager;

namespace BusinessLayer.EmployeesLogic;

public abstract class AbstractEmployeeLogic
{
    internal abstract DalManager Manager { get; }
}
