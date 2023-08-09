using Backups.Algorithms;
using Backups.Enteties;
using Backups.Extra.CleaningAlgorithms;
using Backups.Extra.Merger;
using Backups.Interfaces;
using Backups.Models;
using Xunit;

namespace Backups.Extra.Test;

public class BackupsExtraTests
{
    private const string _path = "/bin";

    [Fact]
    public void MergingTwoSplitRestorePoints()
    {
        const int ExpectedAmountOfStoragesInMergedPoint = 3;

        SplitStorage algo = new ();
        LocalRepository rep = new (_path);
        List<ICleaningAlgorithm> cleaningAlgorithms = new ()
        {
            new NumberCleaningAlgorithm(0),
        };
        Cleaner cleaner = new (cleaningAlgorithms, true);
        RestorePointsMerger merger = new (new SplitMergerTwoRestorePoints());
        Backups.Extra.Logger.Logger logger = new (null, true);
        Backups.Extra.BackupTaskExtra.BackupTaskExtra task = new ("/home/runner/work/Fleack/Split", rep, cleaner, merger);

        BackupObject obj1 = new ("/bin/sed");
        BackupObject obj2 = new ("/bin/kill");
        BackupObject obj3 = new ("/bin/head");

        task.AddBackupObject(obj1);
        task.AddBackupObject(obj2);

        task.Backup("SplitBackup1", algo);

        task.RemoveBackupObject(obj1);
        task.RemoveBackupObject(obj2);
        task.AddBackupObject(obj3);

        task.Backup("SplitBackup2", algo);

        IRestorePoint merged_point = task.StartMerging(task.RestorePoints.ToList(), logger);

        Assert.Equal(ExpectedAmountOfStoragesInMergedPoint, merged_point.Storages.Count);
    }

    [Fact]
    public void MergingTwoSingleRestorePoints()
    {
        const int ExpectedAmountOfStoragesInMergedPoint = 1;

        SingleStorage algo = new ();
        LocalRepository rep = new (_path);
        List<ICleaningAlgorithm> cleaningAlgorithms = new ()
        {
            new NumberCleaningAlgorithm(0),
        };
        Cleaner cleaner = new (cleaningAlgorithms, true);
        RestorePointsMerger merger = new (new SingleMergerTwoRestorePoints());
        Backups.Extra.Logger.Logger logger = new (null, true);
        Backups.Extra.BackupTaskExtra.BackupTaskExtra task = new ("/home/runner/work/Fleack/Single", rep, cleaner, merger);

        BackupObject obj1 = new ("/bin/tail");
        BackupObject obj2 = new ("/bin/grep");
        BackupObject obj3 = new ("/bin/join");

        task.AddBackupObject(obj1);
        task.AddBackupObject(obj2);
        task.AddBackupObject(obj3);

        task.Backup("SingleBackup1", algo);
        Thread.Sleep(5000);
        task.Backup("SingleBackup2", algo);

        IRestorePoint merged_point = task.StartMerging(task.RestorePoints.ToList(), logger);

        Assert.Equal(ExpectedAmountOfStoragesInMergedPoint, merged_point.Storages.Count);
    }

    [Fact]
    public void LoggingInFile()
    {
        const string LoggerFile = "/home/runner/work/Fleack/Log.txt";
        const string ExpectedLogFile =
            "New backup with name: SingleBackup3 was created using algorithm: Backups.Algorithms.SingleStorage\n" +
            "New backup with name: SingleBackup4 was created using algorithm: Backups.Algorithms.SingleStorage\n" +
            "List of IRestorePoints: System.Collections.Generic.List`1[Backups.Interfaces.IRestorePoint] was merged using Backups.Extra.Merger.SingleMergerTwoRestorePoints\n";

        SingleStorage algo = new ();
        LocalRepository rep = new (_path);
        List<ICleaningAlgorithm> cleaningAlgorithms = new ()
        {
            new NumberCleaningAlgorithm(0),
        };
        Cleaner cleaner = new (cleaningAlgorithms, true);
        RestorePointsMerger merger = new (new SingleMergerTwoRestorePoints());
        Backups.Extra.Logger.Logger logger = new (LoggerFile, false);
        Backups.Extra.BackupTaskExtra.BackupTaskExtra task = new ("/home/runner/work/Fleack/Single", rep, cleaner, merger);

        BackupObject obj1 = new ("/bin/id");
        BackupObject obj2 = new ("/bin/info");
        BackupObject obj3 = new ("/bin/yes");

        task.AddBackupObject(obj1);
        task.AddBackupObject(obj2);
        task.AddBackupObject(obj3);

        task.Backup("SingleBackup3", algo, logger);
        Thread.Sleep(5000);
        task.Backup("SingleBackup4", algo, logger);

        _ = task.StartMerging(task.RestorePoints.ToList(), logger);

        logger.Dispose();
        Assert.Equal(ExpectedLogFile, File.ReadAllText(LoggerFile));
    }

    [Fact]
    public void NClearingAlgorithm()
    {
        const int ExpectedAmountOfPoints = 2;
        SingleStorage algo = new ();
        LocalRepository rep = new (_path);
        List<ICleaningAlgorithm> cleaningAlgorithms = new ()
        {
            new NumberCleaningAlgorithm(ExpectedAmountOfPoints),
        };
        Cleaner cleaner = new (cleaningAlgorithms, true);
        RestorePointsMerger merger = new (new SingleMergerTwoRestorePoints());
        Backups.Extra.Logger.Logger logger = new ();
        Backups.Extra.BackupTaskExtra.BackupTaskExtra task = new ("/home/runner/work/Fleack/Single", rep, cleaner, merger);

        BackupObject obj1 = new ("/bin/awk");
        BackupObject obj2 = new ("/bin/ls");
        BackupObject obj3 = new ("/bin/zcat");

        task.AddBackupObject(obj1);
        task.AddBackupObject(obj2);
        task.AddBackupObject(obj3);

        task.Backup("SingleBackup5", algo, logger);
        Thread.Sleep(1000);
        task.Backup("SingleBackup6", algo, logger);
        Thread.Sleep(1000);
        task.Backup("SingleBackup7", algo, logger);
        Thread.Sleep(1000);
        task.Backup("SingleBackup8", algo, logger);
        Thread.Sleep(1000);
        task.Backup("SingleBackup9", algo, logger);

        task.StartCleaning();

        Assert.Equal(ExpectedAmountOfPoints, task.RestorePoints.Count);
    }

    [Fact]
    public void Restoring()
    {
        const string path_to_restore = "/home/runner/work/Fleack/Restored";
        const int ExpectedAmountOfRestoredObjects = 2;
        Backups.Extra.Restorer.Restorer restorer = new ();
        SplitStorage algo = new ();
        LocalRepository rep = new (_path);
        List<ICleaningAlgorithm> cleaningAlgorithms = new ()
        {
            new NumberCleaningAlgorithm(0),
        };
        Cleaner cleaner = new (cleaningAlgorithms, true);
        RestorePointsMerger merger = new (new SplitMergerTwoRestorePoints());
        Backups.Extra.Logger.Logger logger = new ();
        Backups.Extra.BackupTaskExtra.BackupTaskExtra task = new ("/home/runner/work/Fleack/Split", rep, cleaner, merger);

        BackupObject obj1 = new ("/bin/iconv");
        BackupObject obj2 = new ("/bin/sdiff");

        task.AddBackupObject(obj1);
        task.AddBackupObject(obj2);

        task.Backup("SplitBackup", algo, logger);

        IRestorePoint point = task.RestorePoints[0];

        restorer.Restore(point, path_to_restore, logger);

        DirectoryInfo restored_folder = new (path_to_restore);

        Assert.Equal(ExpectedAmountOfRestoredObjects, restored_folder.GetFiles().Length);
    }
}