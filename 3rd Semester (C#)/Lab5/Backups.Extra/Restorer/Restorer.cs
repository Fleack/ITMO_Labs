using Backups.Interfaces;

namespace Backups.Extra.Restorer;

public class Restorer
{
    public void Restore(IRestorePoint restorePoint, string path, Backups.Extra.Logger.Logger logger)
    {
        DirectoryInfo dirInfo = new (path);

        if (!dirInfo.Exists)
        {
            dirInfo.Create();
        }

        foreach (IStorage storage in restorePoint.Storages)
        {
            storage.Zip.Save(Path.Combine(path, storage.Path));
            logger.LogRestored(restorePoint, path, storage);
        }
    }
}
