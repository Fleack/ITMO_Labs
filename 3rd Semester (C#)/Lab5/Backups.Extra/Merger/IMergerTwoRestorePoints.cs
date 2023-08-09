using Backups.Enteties;

namespace Backups.Extra.Merger;

public interface IMergerTwoRestorePoints
{
    RestorePoint? FirstRestorePoint { get; }
    RestorePoint? SecondRestorePoint { get; }

    void SetFirstRestorePoint(RestorePoint? restorePoint);
    void SetSecondRestorePoint(RestorePoint? restorePoint);

    RestorePoint? Merge();
}