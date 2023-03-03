using Backups.Repositories;
using Backups.RestorePoints;

namespace Backups.Extra.RestorePointCleaners;

public class DeleteOldPointsCleaner : IRestorePointsCleaner
{
    public IEnumerable<IRepositoryObject> GetNewPointRepositoryObjects(
        IEnumerable<IRestorePoint> pointToDelete, IRestorePoint lastPoint)
    {
        return lastPoint.Storage.GetEntries();
    }
}