using Backups.Extra.CleaningAlgorithms;
using Backups.Extra.Merger;
using Backups.Extra.Tools;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Extra.BackupTaskExtra;

public class BackupTaskExtra : BackupTask
{
    public BackupTaskExtra(string name, IRepository repository, Cleaner cleaner, RestorePointsMerger merger)
        : base(name, repository)
    {
        if (cleaner is null)
        {
            throw new BackupsExtraException($"Failed to construct BackupTaskExtra. Given value cleaner can not be null");
        }

        if (merger is null)
        {
            throw new BackupsExtraException($"Failed to construct BackupTaskExtra. Given value merger can not be null");
        }

        Cleaner = cleaner;
        Merger = merger;
    }

    public Cleaner Cleaner { get; private set; }
    public RestorePointsMerger Merger { get; private set; }

    public void ChangeMerger(RestorePointsMerger new_merger)
    {
        if (new_merger is null)
        {
            throw new BackupsExtraException($"Failed to ChangeMerger. Given value new_merger can not be null");
        }

        Merger = new_merger;
    }

    public IRestorePoint StartMerging(List<IRestorePoint> restorePoints, Backups.Extra.Logger.Logger logger)
    {
        return Merger.Merge(restorePoints, logger);
    }

    public void ChangeCleaner(Cleaner new_cleaner)
    {
        if (new_cleaner is null)
        {
            throw new BackupsExtraException($"Failed to ChangeCleaner. Given value new_cleaner can not be null");
        }

        Cleaner = new_cleaner;
    }

    public void StartCleaning()
    {
        Cleaner.CleanBackupTask(this);
    }

    public void Backup(string restorePointName, IAlgorithm algorithm, Backups.Extra.Logger.Logger logger)
    {
        Backup(restorePointName, algorithm);
        logger.LogBackuping(restorePointName, algorithm);
    }
}
