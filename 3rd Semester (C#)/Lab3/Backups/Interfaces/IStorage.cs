using Ionic.Zip;

namespace Backups.Interfaces;

public interface IStorage
{
    string Path { get; }
    ZipFile Zip { get; }
    IReadOnlyList<IBackupObject> BackupObjects { get; }

    void AddBackupObject(IBackupObject backupObject);
}