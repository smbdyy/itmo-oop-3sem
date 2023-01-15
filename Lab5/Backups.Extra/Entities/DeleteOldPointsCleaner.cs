using Backups.Extra.Interfaces;
using Backups.Models;
using Backups.Repositories;

namespace Backups.Extra.Entities;

public class DeleteOldPointsCleaner : IRestorePointsCleaner
{
    public IEnumerable<IRepositoryObject> GetNewPointRepositoryObjects(
        IEnumerable<IRestorePoint> pointToDelete, IRestorePoint lastPoint)
    {
        return lastPoint.Storage.GetEntries();
    }
}