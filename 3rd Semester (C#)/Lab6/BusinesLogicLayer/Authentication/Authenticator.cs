using BusinessLayer.EmployeesLogic;
using DataAccessLayer.Entities;
using DataAccessLayer.Manager;

namespace BusinessLayer.Authentication;

internal class Authenticator
{
    internal AbstractEmployeeLogic? Authenticate(DalManager manager, string login, string password)
    {
        AbstractEmployee? employee = manager.LogIn(login, password);
        if (employee == null)
            return null;
        return RecognizeEmployee(employee, manager);
    }

    internal AbstractEmployeeLogic RecognizeEmployee(AbstractEmployee employee, DalManager manager)
    {
        if (employee is Employee)
            return new EmployeeLogic((Employee)employee, manager);
        if (employee is Boss)
            return new BossLogic((Boss)employee, manager);
        throw new BllException($"Failed to RecognizeEmployee: {employee}");
    }
}
