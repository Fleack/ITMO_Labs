using Backups.Enteties;
using Backups.Extra.Tools;
using Backups.Interfaces;

namespace Backups.Extra.Merger;

public class RestorePointsMerger
{
    public RestorePointsMerger(IMergerTwoRestorePoints mergerTwoRestorePoints)
    {
        if (mergerTwoRestorePoints is null)
        {
            throw new BackupsExtraException($"Failed to construct SingleMerger. Given value mergerTwoRestorePoints can not be null");
        }

        MergerTwoRestorePoints = mergerTwoRestorePoints;
    }

    public IMergerTwoRestorePoints MergerTwoRestorePoints { get; private set; }

    public RestorePoint Merge(List<IRestorePoint> restorePoints, Backups.Extra.Logger.Logger logger)
    {
        RestorePoint? newRestorePoint = null;
        foreach (RestorePoint point in restorePoints)
        {
            MergerTwoRestorePoints.SetFirstRestorePoint(newRestorePoint);
            MergerTwoRestorePoints.SetSecondRestorePoint(point);
            newRestorePoint = MergerTwoRestorePoints.Merge();
        }

        if (newRestorePoint is null)
        {
            throw new BackupsExtraException($"Failed to Merge. newRestorePoint can not be null");
        }

        logger.LogMegred(restorePoints, MergerTwoRestorePoints);
        return newRestorePoint;
    }

    public void SetMergerTwoRestorePoints(IMergerTwoRestorePoints newMergerTwoRestorePoints)
    {
        if (newMergerTwoRestorePoints is null)
        {
            throw new BackupsExtraException($"Failed to construct SingleMerger. Given value mergerTwoRestorePoints can not be null");
        }

        MergerTwoRestorePoints = newMergerTwoRestorePoints;
    }
}
