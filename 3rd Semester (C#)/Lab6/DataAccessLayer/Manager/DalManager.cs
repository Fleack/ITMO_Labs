using DataAccessLayer.Entities;
using DataAccessLayer.Messages;
using DataAccessLayer.Reports;

namespace DataAccessLayer.Manager;

public class DalManager
{
    private List<AbstractEmployee> _allEmployees = new ();
    private List<Employee> _regularEmployees = new ();
    private List<Boss> _bosses = new ();
    private List<AbstractMessage> _messages = new ();
    private List<Report> _reports = new ();

    public DalManager() { }

    public void AddNewEmployee(Employee employee)
    {
        if (employee is null)
            throw new DalException("Failed to AddNewEmployee. Given value employee can not be null");
        if (_allEmployees.Any(emp => emp.Login == employee.Login))
            throw new DalException($"Failed to AddNewEmployee. Employee with login: {employee.Login} already exists!");
        _allEmployees.Add(employee);
        _regularEmployees.Add(employee);
    }

    public void RemoveEmployee(Employee employee)
    {
        if (employee is null)
            throw new DalException("Failed to RemoveEmployee. Given value employee can not be null");
        _allEmployees.Remove(employee);
        _regularEmployees.Remove(employee);
    }

    public void AddNewBoss(Boss boss)
    {
        if (boss is null)
            throw new DalException("Failed to AddNewBoss. Given value boss can not be null");
        if (_allEmployees.Any(emp => emp.Login == boss.Login))
            throw new DalException($"Failed to AddNewEmployee. Employee with login: {boss.Login} already exists!");
        _allEmployees.Add(boss);
        _bosses.Add(boss);
    }

    public void RemoveBoss(Boss boss)
    {
        if (boss is null)
            throw new DalException("Failed to RemoveBoss. Given value boss can not be null");
        _allEmployees.Remove(boss);
        _bosses.Remove(boss);
    }

    public void SetHeadToEmployee(AbstractEmployee head, Employee employee)
    {
        if (head is null)
            throw new DalException("Failed to SetHeadToEmployee. Given value head can not be null");
        if (employee is null)
            throw new DalException("Failed to SetHeadToEmployee. Given value employee can not be null");
        employee.SetHead(head);
    }

    public void AddNewMessageToSystem(AbstractMessage message)
    {
        if (message is null)
            throw new DalException("Failed to AddNewMessageToSystem. Given message can not be null");
        _messages.Add(message);
    }

    public void RemoveMessageToSystem(AbstractMessage message)
    {
        if (message is null)
            throw new DalException("Failed to RemoveMessageToSystem. Given message can not be null");
        _messages.Remove(message);
    }

    public void AddNewFormedReport(Report report)
    {
        if (report is null)
            throw new DalException("Failed to AddNewFormedReport. Given report can not be null");
        _reports.Add(report);
    }

    public void RemoveFormedReport(Report report)
    {
        if (report is null)
            throw new DalException("Failed to RemoveFormedReport. Given report can not be null");
        _reports.Remove(report);
    }

    public void AddSubordinateToAbstractEmployee(AbstractEmployee abstractEmployee, Employee employee)
    {
        if (abstractEmployee is null)
            throw new DalException("Failed to AddSubordinateToAbstractEmployee. Given abstractEmployee can not be null");
        abstractEmployee.AddSubordinate(employee);
    }

    public void RemoveSubordinateToAbstractEmployee(AbstractEmployee abstractEmployee, Employee employee)
    {
        if (abstractEmployee is null)
            throw new DalException("Failed to AddSubordinateToAbstractEmployee. Given abstractEmployee can not be null");
        abstractEmployee.RemoveSubordinate(employee);
    }

    public void SetAnswerToMessage(AbstractMessage message, string answer)
    {
        message.SetAnswer(answer);
    }

    public void MarkMessageAsProcessed(AbstractMessage message)
    {
        message.SetStateToProcessed();
    }

    public AbstractEmployee? LogIn(string login, string password)
    {
        if (string.IsNullOrEmpty(login))
            throw new DalException("Failed to FindEmployee. Given report login can not be null or empty");
        if (string.IsNullOrEmpty(password))
            throw new DalException("Failed to FindEmployee. Given report password can not be null or empty");

        AbstractEmployee? employee = _allEmployees.FirstOrDefault(emp => emp.Login == login && emp.Password == password);
        employee?.ResetProgress();
        return employee;
    }

    public AbstractMessage? FindMessage(Guid id)
    {
        return _messages.FirstOrDefault(msg => msg.ID == id);
    }

    public AbstractEmployee? FindEmployee(Guid id)
    {
        return _allEmployees.FirstOrDefault(emp => emp.ID == id);
    }

    public Report? FindReport(Guid id)
    {
        return _reports.FirstOrDefault(report => report.ReportId == id);
    }

    public List<AbstractMessage> GetAllMessagesOfEmployee(Employee employee)
    {
        return _messages.Where(msg => msg.AccessLevel <= employee.AccessLevel).ToList();
    }

    public List<AbstractMessage> GetAllNewMessagesOfEmployee(Employee employee)
    {
        List<AbstractMessage> messages = GetAllTypedMessage(MessageState.New, employee);
        messages.ForEach(msg => msg.SetStateToProcessed());
        return messages;
    }

    public List<AbstractMessage> GetAllRecievedMessages(Employee employee)
    {
        return GetAllTypedMessage(MessageState.Recieved, employee);
    }

    public List<AbstractMessage> GetAllProcessedMessages(Employee employee)
    {
        return GetAllTypedMessage(MessageState.Processed, employee);
    }

    public IReadOnlyList<Report> GetAllReports(Boss boss)
    {
        return _reports.Where(report => report.Author.Login == boss.Login
        && report.Author.Password == boss.Password)
            .ToList();
    }

    public IReadOnlyList<Report> GetReportsByDate(Boss boss, DateOnly date)
    {
        return _reports.Where(report => report.ReportDate == date
        && report.Author.Login == boss.Login
        && report.Author.Password == boss.Password)
            .ToList();
    }

    private List<AbstractMessage> GetAllTypedMessage(MessageState messageType, Employee employee)
    {
        return _messages.Where(message => message.State == messageType && message.AccessLevel <= employee.AccessLevel).ToList();
    }
}