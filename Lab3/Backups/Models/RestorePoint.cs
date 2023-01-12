using Backups.StorageAlgorithms;

namespace Backups.Models;

public class RestorePoint : IRestorePoint
{
    private readonly List<IBackupObject> _backupObjects;

    public RestorePoint(string folderName, IEnumerable<IBackupObject> backupObjects, IStorage storage)
    {
        if (folderName == string.Empty)
        {
            throw new NotImplementedException();
        }

        FolderName = folderName;
        _backupObjects = backupObjects.ToList();
        Storage = storage;
    }

    public string FolderName { get; }
    public IReadOnlyCollection<IBackupObject> BackupObjects => _backupObjects;
    public DateTime CreationDateTime { get; } = DateTime.Now;
    public IStorage Storage { get; }
}