using Backups.Interfaces;

namespace Backups.Extra.CleaningAlgorithms;

public class DateCleaningAlgorithm : ICleaningAlgorithm
{
    public DateCleaningAlgorithm(DateTime restorePointsDeadline)
    {
        RestorePointsDeadline = restorePointsDeadline;
    }

    public DateTime RestorePointsDeadline { get; }

    public List<IRestorePoint> SelectRestorePointsToDelete(IReadOnlyList<IRestorePoint> restorePoints)
    {
        List<IRestorePoint> expiredRestorePoint = new ();

        foreach (IRestorePoint point in restorePoints)
        {
            if (point.DateAndTime < RestorePointsDeadline)
            {
                expiredRestorePoint.Add(point);
            }
        }

        return expiredRestorePoint;
    }
}
