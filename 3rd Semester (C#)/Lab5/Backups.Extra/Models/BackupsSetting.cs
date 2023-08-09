using Backups.Extra.Tools;
using Backups.Interfaces;

namespace Backups.Extra.Models;

public class BackupsSetting
{
    private List<IBackupTask> _backupTasks;

    public BackupsSetting(List<IBackupTask> backupTasks)
    {
        _backupTasks = backupTasks;
        _backupTasks ??= new ();
    }

    public IReadOnlyList<IBackupTask> BackupTasks => _backupTasks;

    public void AddBackupTask(IBackupTask backupTask)
    {
        if (backupTask is null)
        {
            throw new BackupsExtraException($"Failed to AddBackupTask. Given value backupTask can not be null");
        }

        _backupTasks.Add(backupTask);
    }

    public void RemoveBackupTask(IBackupTask backupTask)
    {
        if (backupTask is null)
        {
            throw new BackupsExtraException($"Failed to AddBackupTask. Given value backupTask can not be null");
        }

        _backupTasks.Remove(backupTask);
    }
}
