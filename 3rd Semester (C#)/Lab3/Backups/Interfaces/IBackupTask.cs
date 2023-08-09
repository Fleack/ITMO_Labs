namespace Backups.Interfaces;

public interface IBackupTask
{
    string Name { get; }
    IRepository Repository { get; }

    IReadOnlyList<IBackupObject> BackupObjects { get; }
    IReadOnlyList<IRestorePoint> RestorePoints { get; }

    void AddBackupObject(IBackupObject backupObject);
    void RemoveBackupObject(IBackupObject backupObject);
    void AddRestorePoint(IRestorePoint restorePoint);
    void RemoveRestorePoint(IRestorePoint restorePoint);
    void Backup(string restorePointName, IAlgorithm algorithm);
}