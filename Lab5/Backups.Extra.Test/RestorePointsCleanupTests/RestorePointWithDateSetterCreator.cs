using Backups.Models;
using Backups.RestorePoints;
using Backups.RestorePoints.Creators;
using Backups.StorageAlgorithms;

namespace Backups.Extra.Test.RestorePointsCleanupTests;

public class RestorePointWithDateSetterCreator : IRestorePointCreator
{
    public IRestorePoint Create(string folderName, IEnumerable<IBackupObject> backupObjects, IStorage storage)
    {
        return new RestorePointWithDateSetter(folderName, backupObjects, storage);
    }
}