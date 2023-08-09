using Backups.Enteties;
using Backups.Exceptions;
using Backups.Interfaces;
using Ionic.Zip;

namespace Backups.Algorithms;

public class SplitStorage : IAlgorithm
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

        List<IStorage> list = new ();

        foreach (IBackupObject backupObject in backupTask.BackupObjects)
        {
            ZipFile zip = new ();
            zip.AddFile(backupObject.FilePath);

            Storage storage = new (backupObject.FileName + $"-{backup_cnt}", zip);
            storage.AddBackupObject(backupObject);

            list.Add(storage);
        }

        RestorePoint newRestorePoint = new (restorePointName, DateTime.Now, list, backup_cnt);
        backupTask.AddRestorePoint(newRestorePoint);
    }
}