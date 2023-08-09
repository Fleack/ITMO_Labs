namespace Backups.Interfaces;

public interface IAlgorithm
{
    void Backup(IBackupTask backupTask, string restorePointName, int backup_cnt);
}