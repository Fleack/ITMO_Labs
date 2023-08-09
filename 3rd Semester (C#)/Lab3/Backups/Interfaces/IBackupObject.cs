namespace Backups.Interfaces;

public interface IBackupObject
{
    string FileName { get; }
    string FilePath { get; }
}