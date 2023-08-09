using DataAccessLayer.Entities;
using DataAccessLayer.Manager;
using DataAccessLayer.Messages;

namespace BusinessLayer.EmployeesLogic;

public class EmployeeLogic : AbstractEmployeeLogic
{
    private readonly Employee _employee;

    internal EmployeeLogic(Employee employee, DalManager manager)
    {
        if (employee is null)
            throw new BllException("Failed to construct EmployeeLogic. Given value employee can not be null");
        if (manager is null)
            throw new BllException("Failed to construct EmployeeLogic. Given value manager can not be null");
        _employee = employee;
        Manager = manager;
    }

    public Employee Employee => _employee;
    internal override DalManager Manager { get; }

    internal void AnswerMessage(AbstractMessage message, string answer)
    {
        Manager.SetAnswerToMessage(message, answer);
        _employee.IncreaseMessagesCount();
    }

    internal void MarkMessageAs(AbstractMessage message)
    {
        Manager.MarkMessageAsProcessed(message);
        _employee.IncreaseMessagesCount();
    }
}
