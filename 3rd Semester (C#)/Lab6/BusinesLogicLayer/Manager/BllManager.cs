using BusinessLayer.Authentication;
using BusinessLayer.EmployeesLogic;
using BusinessLayer.Serializer;
using DataAccessLayer.Entities;
using DataAccessLayer.Manager;
using DataAccessLayer.Messages;
using DataAccessLayer.MessageSources;
using DataAccessLayer.Reports;

namespace BusinessLayer.Manager;

public class BllManager
{
    private DalManager manager = new DalManager();

    public AbstractEmployeeLogic? LogIn(string login, string password)
    {
        Authenticator auth = new ();
        return auth.Authenticate(manager, login, password);
    }

    public AbstractMessage? FindMessageByID(Guid id)
    {
        return manager.FindMessage(id);
    }

    public EmployeeLogic CreateNewEmployee(
        string login, string password, uint accessLevel, List<Employee> subordinates, AbstractEmployee head)
    {
        Employee employee = new (login, password, accessLevel, subordinates, head);
        manager.AddNewEmployee(employee);
        EmployeeLogic logic = new (employee, manager);
        head.AddSubordinate(employee);

        return logic;
    }

    public BossLogic CreateNewBoss(
        string login, string password, uint accessLevel, List<Employee> subordinates)
    {
        Boss boss = new (login, password, accessLevel, subordinates);
        manager.AddNewBoss(boss);
        BossLogic logic = new (boss, manager);

        return logic;
    }

    public IReadOnlyList<AbstractMessage> GetAllMessagesOfEmployee(EmployeeLogic logic)
    {
        return manager.GetAllMessagesOfEmployee(logic.Employee);
    }

    public IReadOnlyList<AbstractMessage> GetAllNewMessagesOfEmployee(EmployeeLogic logic)
    {
        return manager.GetAllNewMessagesOfEmployee(logic.Employee);
    }

    public IReadOnlyList<AbstractMessage> GetAllProcessedMessages(EmployeeLogic logic)
    {
        return manager.GetAllProcessedMessages(logic.Employee);
    }

    public IReadOnlyList<AbstractMessage> GetAllRecievedMessages(EmployeeLogic logic)
    {
        return manager.GetAllRecievedMessages(logic.Employee);
    }

    public void AnswerMessage(EmployeeLogic logic, AbstractMessage msg, string answer)
    {
        logic.AnswerMessage(msg, answer);
    }

    public void MarkMessageAsProcessed(EmployeeLogic logic, AbstractMessage msg)
    {
        logic.MarkMessageAs(msg);
    }

    public void FormReport(BossLogic boss, DateOnly date)
    {
        boss.FormReport(date);
    }

    public IReadOnlyList<Report> GetListOfReports(BossLogic boss)
    {
        return manager.GetAllReports(boss.Boss);
    }

    public IReadOnlyList<Report> GetListOfReportsOfDate(BossLogic boss, DateOnly date)
    {
        return manager.GetReportsByDate(boss.Boss, date);
    }

    public void SaveSystemSettings(string path)
    {
        SystemSerialyzer serialyzer = new (path);
        serialyzer.Serialize(manager);
    }

    public void RecoverSystemSettings(string path)
    {
        SystemSerialyzer serialyzer = new (path);
        manager = serialyzer.Deserialize();
    }

    public AbstractMessage SendMessageToSystem(IMessageSource source, string text, uint accessLevel)
    {
        if (source is null)
            throw new BllException("Failed to SendMessageToSystem. Value source can not be null");
        if (string.IsNullOrWhiteSpace(text))
            throw new BllException("Failed to SendMessageToSystem. Value mstextg can not be null");

        AbstractMessage message = source.SendMessage(text, accessLevel);
        manager.AddNewMessageToSystem(message);

        return message;
    }
}
