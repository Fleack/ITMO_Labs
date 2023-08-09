using Backups.Interfaces;

namespace Backups.Extra.CleaningAlgorithms;

public interface ICleaningAlgorithm
{
    List<IRestorePoint> SelectRestorePointsToDelete(IReadOnlyList<IRestorePoint> restorePoints);
}
