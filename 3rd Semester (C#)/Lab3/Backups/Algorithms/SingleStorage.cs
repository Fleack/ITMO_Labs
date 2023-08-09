using Backups.Enteties;
using Backups.Exceptions;
using Backups.Interfaces;
using Ionic.Zip;

namespace Backups.Algorithms;

public class SingleStorage : IAlgorithm
{
    public void Backup(IBackupTask backupTask, string restorePointName, int backup_cnt)
    {
        if (backupTask is null)
        {
            throw new BackupsException("Give value backupTask can not be null");
        }

        if (string.IsNullOrWhiteSpace(restorePointName))
        {
            throw new BackupsException($"Give value {restorePointName} can not be null or white space");
        }

        ZipFile zip = new ();
        Storage storage = new ($"Backup-{backup_cnt}", zip);

        foreach (IBackupObject backupObject in backupTask.BackupObjects)
        {
            zip.AddFile(backupObject.FilePath);
            storage.AddBackupObject(backupObject);
        }

        List<IStorage> list = new ()
        {
            storage,
        };

        RestorePoint newRestorePoint = new (restorePointName, DateTime.Now, list, backup_cnt);
        backupTask.AddRestorePoint(newRestorePoint);
    }
}