using Backups.Repositories;
using Backups.RestorePoints;

namespace Backups.Extra.RestorePointCleaners;

public class MergePointsCleaner : IRestorePointsCleaner
{
    public IEnumerable<IRepositoryObject> GetNewPointRepositoryObjects(
        IEnumerable<IRestorePoint> pointToDelete, IRestorePoint lastPoint)
    {
        var newPointRepositoryObjects = lastPoint.Storage.GetEntries().ToList();
        var pointsToDeleteAsList = pointToDelete.ToList();
        pointsToDeleteAsList.Reverse();

        foreach (IRestorePoint point in pointsToDeleteAsList)
        {
            IEnumerable<IRepositoryObject> pointStorageEntries = point.Storage.GetEntries();
            foreach (IRepositoryObject storageEntry in pointStorageEntries)
            {
                if (newPointRepositoryObjects.All(o => o.Path != storageEntry.Path))
                {
                    newPointRepositoryObjects.Add(storageEntry);
                }
            }
        }

        return newPointRepositoryObjects;
    }
}