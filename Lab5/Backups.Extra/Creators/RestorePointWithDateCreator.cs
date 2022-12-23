using Backups.Extra.Models;
using Backups.Models;
using Backups.StorageAlgorithms;
using Backups.Tools.Creators;

namespace Backups.Extra.Creators;

public class RestorePointWithDateCreator : IRestorePointCreator
{
    private DateTime _dateTime = DateTime.Now;

    public void SetDateTime(DateTime dateTime)
    {
        _dateTime = dateTime;
    }

    public IRestorePoint Create(IEnumerable<IBackupObject> backupObjects, IStorage storage)
    {
        return new RestorePointWithDate(backupObjects, storage, _dateTime);
    }
}