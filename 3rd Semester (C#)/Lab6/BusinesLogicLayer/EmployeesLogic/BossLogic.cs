using DataAccessLayer.Entities;
using DataAccessLayer.Manager;
using DataAccessLayer.Reports;

namespace BusinessLayer.EmployeesLogic;

public class BossLogic : AbstractEmployeeLogic
{
    private readonly Boss _boss;

    internal BossLogic(Boss boss, DalManager manager)
    {
        if (boss is null)
            throw new BllException("Failed to construct BossLogic. Given value boss can not be null");
        if (manager is null)
            throw new BllException("Failed to construct BossLogic. Given value manager can not be null");

        _boss = boss;
        Manager = manager;
    }

    public Boss Boss => _boss;
    internal override DalManager Manager { get; }

    internal void FormReport(DateOnly reportDate)
    {
        uint markedMessagesCount = (uint)_boss.GetSubordinates().Sum(emp => emp.ProcessedMessagesCount);
        Report report = new (markedMessagesCount, reportDate, Boss);
        _boss.IncreaseReportsCount();
        Manager.AddNewFormedReport(report);
    }
}
