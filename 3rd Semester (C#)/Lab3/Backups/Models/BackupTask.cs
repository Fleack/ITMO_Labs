using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Models;

public class BackupTask : IBackupTask
{
    private int _backups_counter = 0;
    private List<IBackupObject> _backupObjects = new ();
    private List<IRestorePoint> _restorePoints = new ();

    public BackupTask(string name, IRepository repository)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BackupsException($"Given value {name} can not be null or white space");
        }

        if (repository is null)
        {
            throw new BackupsException($"Given value {repository} can not be null");
        }

        Name = name;
        Repository = repository;
    }

    public int BackupsCounter { get { return _backups_counter; } }
    public string Name { get; }
    public IRepository Repository { get; }
    public IReadOnlyList<IBackupObject> BackupObjects => _backupObjects;
    public IReadOnlyList<IRestorePoint> RestorePoints => _restorePoints;

    public void AddBackupObject(IBackupObject backupObject)
    {
        if (backupObject is null)
        {
            throw new BackupsException($"Given value {backupObject} can not be null");
        }

        _backupObjects.Add(backupObject);
    }

    public void AddRestorePoint(IRestorePoint restorePoint)
    {
        if (restorePoint is null)
        {
            throw new BackupsException($"Given value {restorePoint} can not be null");
        }

        _restorePoints.Add(restorePoint);
    }

    public void RemoveBackupObject(IBackupObject backupObject)
    {
        if (backupObject is null)
        {
            throw new BackupsException($"Given value {backupObject} can not be null");
        }

        _backupObjects.Remove(backupObject);
    }

    public void RemoveRestorePoint(IRestorePoint restorePoint)
    {
        if (restorePoint is null)
        {
            throw new BackupsException($"Given value {restorePoint} can not be null");
        }

        _restorePoints.Remove(restorePoint);
    }

    public void Backup(string restorePointName, IAlgorithm algorithm)
    {
        if (string.IsNullOrWhiteSpace(restorePointName))
        {
            throw new BackupsException($"Given value {restorePointName} can not be null");
        }

        algorithm.Backup(this, restorePointName, _backups_counter);
        _backups_counter++;
        Repository.Save(this);
    }
}