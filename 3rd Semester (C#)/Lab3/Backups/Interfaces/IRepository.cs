namespace Backups.Interfaces;

public interface IRepository
{
    void Save(IBackupTask backupTask);
}
