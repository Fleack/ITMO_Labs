using Backups.Extra.Merger;
using Backups.Extra.Models;
using Backups.Extra.Tools;
using Backups.Interfaces;

namespace Backups.Extra.Logger;

public class Logger : IDisposable
{
    private StreamWriter? _fileStream;

    public Logger(string? file_name = null, bool prefix = false)
    {
        if (string.IsNullOrWhiteSpace(file_name))
        {
            _fileStream = null;
        }
        else
        {
            _fileStream = new (file_name, append: true);
        }

        Prefix = prefix;
    }

    public bool Prefix { get; }

    public void Dispose()
    {
        _fileStream?.Close();
        _fileStream?.Dispose();
    }

    public void LogRestorePointCreated(IRestorePoint point)
    {
        string msg = $"New IRestorePoint was created: {point.ToString()}";
        Log(msg);
    }

    public void LogStorageCreated(IStorage storage)
    {
        string msg = $"New IStorage was created: {storage.ToString()}";
        Log(msg);
    }

    public void LogBackuping(string restorePointName, IAlgorithm algorithm)
    {
        string msg = $"New backup with name: {restorePointName} was created using algorithm: {algorithm.ToString()}";
        Log(msg);
    }

    public void LogMegred(List<IRestorePoint> restorePoints, IMergerTwoRestorePoints mergerTwoRestorePoints)
    {
        string msg = $"List of IRestorePoints: {restorePoints.ToString()} was merged using {mergerTwoRestorePoints.ToString()}";
        Log(msg);
    }

    public void LogSerialized(BackupsSetting backupsSetting, string settingPath)
    {
        string msg = $"BackupsSetting: {backupsSetting.ToString()} was serialized to {settingPath}";
        Log(msg);
    }

    public void LogDeserialized(BackupsSetting backupsSetting, string settingPath)
    {
        string msg = $"BackupsSetting: {backupsSetting.ToString()} was deserialized from {settingPath}";
        Log(msg);
    }

    public void LogRestored(IRestorePoint restorePoint, string path, IStorage storage)
    {
        string msg = $"Storage: {storage.ToString()} from RestorePoint: {restorePoint.ToString()} was restored to path: {path}";
        Log(msg);
    }

    private void Log(string msg)
    {
        if (Prefix)
        {
            msg = DateTime.Now.ToString() + ": " + msg;
        }

        if (_fileStream is not null)
        {
            LogIntoFile(msg);
        }
        else
        {
            LogIntoConsole(msg);
        }
    }

    private void LogIntoFile(string msg)
    {
        if (_fileStream is null)
        {
            throw new BackupsExtraException($"Failed to LogIntoFile. _fileStream is null");
        }

        _fileStream.WriteLine(msg);
        _fileStream.Flush();
    }

    private void LogIntoConsole(string msg)
    {
        Console.WriteLine(msg);
    }
}
