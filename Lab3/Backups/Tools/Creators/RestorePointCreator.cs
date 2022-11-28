using Backups.Models;
using Backups.StorageAlgorithms;

namespace Backups.Tools.Creators;

public class RestorePointCreator : IRestorePointCreator
{
    public IRestorePoint Create(IEnumerable<IBackupObject> backupObjects, IStorage storage)
    {
        return new RestorePoint(backupObjects, storage);
    }
}