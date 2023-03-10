using Backups.Models;
using Backups.RestorePoints;
using Backups.StorageAlgorithms;

namespace Backups.Extra.Test.RestorePointsCleanupTests;

public class RestorePointWithDateSetter : IRestorePoint
{
    private List<IBackupObject> _backupObjects;

    public RestorePointWithDateSetter(
        string folderName,
        IEnumerable<IBackupObject> backupObjects,
        IStorage storage)
    {
        if (folderName == string.Empty)
        {
            throw new NotImplementedException();
        }

        _backupObjects = backupObjects.ToList();
        Storage = storage;
        FolderName = folderName;
    }

    public IReadOnlyCollection<IBackupObject> BackupObjects => _backupObjects;
    public DateTime CreationDateTime { get; set; } = DateTime.Now;
    public IStorage Storage { get; }
    public string FolderName { get; }
}