using Backups.Extra.Tools;
using Backups.Interfaces;

namespace Backups.Extra.CleaningAlgorithms;

public class NumberCleaningAlgorithm : ICleaningAlgorithm
{
    private const int MinRestorePointAmount = 0;

    public NumberCleaningAlgorithm(int maxRestorePointsAmount)
    {
        if (maxRestorePointsAmount < MinRestorePointAmount)
        {
            throw new BackupsExtraException($"Failed to construct NCleaningAlgorithm. Given value maxRestorePointsAmount: {maxRestorePointsAmount} less than MinRestorePointAmount: {MinRestorePointAmount}");
        }

        MaxRestorePointsAmount = maxRestorePointsAmount;
    }

    public int MaxRestorePointsAmount { get; private set; }

    public List<IRestorePoint> SelectRestorePointsToDelete(IReadOnlyList<IRestorePoint> restorePoints)
    {
        if (restorePoints.Count < MaxRestorePointsAmount)
        {
            return new List<IRestorePoint>();
        }

        var restorePointsToDelete = new List<IRestorePoint>(restorePoints);
        restorePointsToDelete.Sort((point1, point2) => point1.DateAndTime.CompareTo(point2.DateAndTime));

        return restorePointsToDelete.GetRange(0, restorePointsToDelete.Count - MaxRestorePointsAmount);
    }
}
