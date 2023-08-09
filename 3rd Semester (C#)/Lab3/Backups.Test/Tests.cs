using Backups.Algorithms;
using Backups.Enteties;
using Backups.Interfaces;
using Backups.Models;
using Xunit;

namespace Backups.Tests;

public class Tests
{
    private const string _path = "/bin";

    [Fact]
    public void SplitBackuping()
    {
        const int ExpectedAmountOfStorages = 3;
        const int ExpectedAmountOfRepositories = 2;

        SplitStorage algo = new ();
        LocalRepository rep = new (_path);
        BackupTask task = new ("/home/runner/work/Fleack/Split", rep);

        BackupObject obj1 = new ("/bin/sed");
        BackupObject obj2 = new ("/bin/kill");

        task.AddBackupObject(obj1);
        task.AddBackupObject(obj2);

        task.Backup("SplitBackup", algo);

        task.RemoveBackupObject(obj1);

        task.Backup("SplitBackup", algo);

        int realAmountOfStorages = 0;
        foreach (IRestorePoint restorePoint in task.RestorePoints)
        {
            realAmountOfStorages += restorePoint.Storages.Count;
        }

        Assert.Equal(task.RestorePoints.Count, ExpectedAmountOfRepositories);
        Assert.Equal(realAmountOfStorages, ExpectedAmountOfStorages);
    }

    [Fact]
    public void SingleBackuping()
    {
        const int ExpectedAmountOfStorages = 1;
        const int ExpectedAmountOfRepositories = 1;

        SingleStorage algo = new ();
        LocalRepository rep = new (_path);
        BackupTask task = new ("/home/runner/work/Fleack/Single", rep);

        BackupObject obj1 = new ("/bin/scp");
        BackupObject obj2 = new ("/bin/join");

        task.AddBackupObject(obj1);
        task.AddBackupObject(obj2);

        task.Backup("SingleBackup", algo);

        int realAmountOfStorages = 0;
        foreach (IRestorePoint restorePoint in task.RestorePoints)
        {
            realAmountOfStorages += restorePoint.Storages.Count;
        }

        Assert.Equal(task.RestorePoints.Count, ExpectedAmountOfRepositories);
        Assert.Equal(realAmountOfStorages, ExpectedAmountOfStorages);
    }
}