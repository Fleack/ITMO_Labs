using Backups.Enteties;
using Backups.Extra.Tools;
using Backups.Interfaces;

namespace Backups.Extra.Merger;

public class SplitMergerTwoRestorePoints : IMergerTwoRestorePoints
{
    public RestorePoint? FirstRestorePoint { get; private set; }
    public RestorePoint? SecondRestorePoint { get; private set; }

    public void SetFirstRestorePoint(RestorePoint? restorePoint)
    {
        FirstRestorePoint = restorePoint;
    }

    public void SetSecondRestorePoint(RestorePoint? restorePoint)
    {
        SecondRestorePoint = restorePoint;
    }

    public RestorePoint? Merge()
    {
        if (FirstRestorePoint is null && SecondRestorePoint is null)
        {
            throw new BackupsExtraException($"Failed to Merge. At least one of IRestorePoint has to be non-null");
        }

        if (FirstRestorePoint is null)
        {
            return SecondRestorePoint;
        }

        if (SecondRestorePoint is null)
        {
            return FirstRestorePoint;
        }

        if (FirstRestorePoint.DateAndTime > SecondRestorePoint.DateAndTime)
        {
            return MergeNotNullRestorePoint(SecondRestorePoint, FirstRestorePoint);
        }

        return MergeNotNullRestorePoint(FirstRestorePoint, SecondRestorePoint);
    }

    private RestorePoint MergeNotNullRestorePoint(RestorePoint oldRestorePoint, RestorePoint newRestorePoint)
    {
        var new_storage = new List<IStorage>(newRestorePoint.Storages);
        foreach (IStorage storage in oldRestorePoint.Storages)
        {
            if (!new_storage.Contains(storage))
            {
                new_storage.Add(storage);
            }
        }

        return new RestorePoint(newRestorePoint.Name, newRestorePoint.DateAndTime, new_storage, newRestorePoint.Name.Last() + 1);
    }
}
