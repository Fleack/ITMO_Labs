using Backups.Exceptions;
using Backups.Interfaces;
using Ionic.Zip;

namespace Backups.Enteties;

public class Storage : IStorage
{
    private List<IBackupObject> _backupObjects = new ();

    public Storage(string path, ZipFile zip)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new BackupsException($"Given value {path} can not be null or white space");
        }

        if (zip is null)
        {
            throw new BackupsException($"Given value {zip} can not be null");
        }

        Path = path;
        Zip = zip;
    }

    public string Path { get; }
    public ZipFile Zip { get; }
    public IReadOnlyList<IBackupObject> BackupObjects => _backupObjects;

    public void AddBackupObject(IBackupObject backupObject)
    {
        if (backupObject is null)
        {
            throw new BackupsException($"Given value {backupObject} can not be null");
        }

        _backupObjects.Add(backupObject);
    }
}