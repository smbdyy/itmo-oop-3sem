using Backups.Repositories;
using Backups.RestorePoints;

namespace Backups.Extra.RestorePointCleaners;

public interface IRestorePointsCleaner
{
    public IEnumerable<IRepositoryObject> GetNewPointRepositoryObjects(
        IEnumerable<IRestorePoint> pointToDelete, IRestorePoint lastPoint);
}