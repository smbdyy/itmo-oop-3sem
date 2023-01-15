using Backups.Models;
using Backups.StorageAlgorithms;

namespace Backups.Extra.Models;

public class RestorePointWithDate : IRestorePoint
{
    private List<IBackupObject> _backupObjects;

    public RestorePointWithDate(
        string folderName,
        IEnumerable<IBackupObject> backupObjects,
        IStorage storage,
        DateTime creationDateTime)
    {
        if (folderName == string.Empty)
        {
            throw new NotImplementedException();
        }

        _backupObjects = backupObjects.ToList();
        Storage = storage;
        CreationDateTime = creationDateTime;
        FolderName = folderName;
    }

    public IReadOnlyCollection<IBackupObject> BackupObjects => _backupObjects;
    public DateTime CreationDateTime { get; }
    public IStorage Storage { get; }
    public string FolderName { get; }
}