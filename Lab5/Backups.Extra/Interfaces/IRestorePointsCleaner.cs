using Backups.Models;
using Backups.Repositories;
using Backups.RestorePoints;
using Backups.StorageAlgorithms;

namespace Backups.Extra.Interfaces;

public interface IRestorePointsCleaner
{
    public IEnumerable<IRepositoryObject> GetNewPointRepositoryObjects(
        IEnumerable<IRestorePoint> pointToDelete, IRestorePoint lastPoint);
}