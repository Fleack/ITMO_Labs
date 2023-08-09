using Backups.Enteties;
using Backups.Extra.Tools;

namespace Backups.Extra.Merger;

public class SingleMergerTwoRestorePoints : IMergerTwoRestorePoints
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
            return FirstRestorePoint;
        }

        return SecondRestorePoint;
    }
}
