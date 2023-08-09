using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Enteties;

public class BackupObject : IBackupObject
{
    public BackupObject(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new BackupsException($"Given value {filePath} can not be null or white space");
        }

        FilePath = filePath;
        FileName = Path.GetFileNameWithoutExtension(filePath);
    }

    public string FileName { get; }
    public string FilePath { get; }
}