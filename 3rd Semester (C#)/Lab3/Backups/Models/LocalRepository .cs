using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Models;
public class LocalRepository : IRepository
{
    public LocalRepository(string repositoryPath)
    {
        if (string.IsNullOrWhiteSpace(repositoryPath))
        {
            throw new BackupsException($"Given value {repositoryPath} can not be null or white space");
        }

        RepositoryPath = repositoryPath;
        DirInfo = new DirectoryInfo(repositoryPath);
    }

    public DirectoryInfo DirInfo { get; }
    public string RepositoryPath { get; }

    public void Save(IBackupTask backupTask)
    {
        if (backupTask is null)
        {
            throw new BackupsException($"Given value {backupTask} can not be null");
        }

        string path = Path.Join(backupTask.Name, backupTask.RestorePoints.Last().Name);
        DirectoryInfo directoryInfo = new (path);

        if (!directoryInfo.Exists)
        {
            directoryInfo.Create();
        }

        foreach (IStorage storage in backupTask.RestorePoints.Last().Storages)
        {
            storage.Zip.Save(Path.Join(path, storage.Path));
        }
    }
}