using DataAccessLayer.Entities;

namespace DataAccessLayer.Reports;

public class Report
{
    public Report(uint processedMessagesAmount, DateOnly reportDate, Boss boss)
    {
        if (boss is null)
            throw new DalException("Failed to construct Report.");
        ReportId = Guid.NewGuid();
        ProcessedMessagesAmount = processedMessagesAmount;
        ReportDate = reportDate;
        Author = new Boss(boss.Login, boss.Password, boss.AccessLevel, boss.GetSubordinates().ToList());
    }

    internal Boss Author { get; }
    internal DateOnly ReportDate { get; }
    internal Guid ReportId { get; }
    internal uint ProcessedMessagesAmount { get; }
}
