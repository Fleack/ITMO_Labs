using Backups.Extra.Tools;
using Backups.Interfaces;

namespace Backups.Extra.CleaningAlgorithms;

public class Cleaner
{
    private List<ICleaningAlgorithm> _cleaningAlgorithms;

    public Cleaner(List<ICleaningAlgorithm> cleaningAlgorithms, bool algorithm_any)
    {
        if (cleaningAlgorithms is null)
        {
            throw new BackupsExtraException($"Failed to construct Cleaner. Given value cleaningAlgorithms can not be null");
        }

        _cleaningAlgorithms = cleaningAlgorithms;
        AlgorithmAny = algorithm_any;
    }

    public bool AlgorithmAny { get; }

    public void CleanBackupTask(IBackupTask backupTask)
    {
        if (backupTask is null)
        {
            throw new BackupsExtraException($"Failed to CleanBackupTask. Given value backupTask can not be null");
        }

        if (AlgorithmAny)
        {
            AnyAlgorithmSuitable(backupTask);
        }
        else
        {
            AllAlgorithmsSuitable(backupTask);
        }
    }

    private void AnyAlgorithmSuitable(IBackupTask backupTask)
    {
        List<IRestorePoint> cleanedRestorePointsList = new ();
        foreach (ICleaningAlgorithm algorithm in _cleaningAlgorithms)
        {
            cleanedRestorePointsList = cleanedRestorePointsList.Concat(algorithm.SelectRestorePointsToDelete(backupTask.RestorePoints)).ToList();
        }

        cleanedRestorePointsList = cleanedRestorePointsList.Distinct().ToList();
        RemoveRestorePointsFromBackupTask(backupTask, cleanedRestorePointsList);
    }

    private void AllAlgorithmsSuitable(IBackupTask backupTask)
    {
        var cleanedRestorePointsList = backupTask.RestorePoints.ToList();
        foreach (ICleaningAlgorithm algorithm in _cleaningAlgorithms)
        {
            cleanedRestorePointsList = cleanedRestorePointsList.Intersect(algorithm.SelectRestorePointsToDelete(backupTask.RestorePoints)).ToList();
        }

        RemoveRestorePointsFromBackupTask(backupTask, cleanedRestorePointsList);
    }

    private void RemoveRestorePointsFromBackupTask(IBackupTask backupTask, List<IRestorePoint> restorePointsToDelete)
    {
        foreach (IRestorePoint point in restorePointsToDelete)
        {
            backupTask.RemoveRestorePoint(point);
        }
    }
}
