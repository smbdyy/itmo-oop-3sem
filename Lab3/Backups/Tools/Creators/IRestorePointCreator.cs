using Backups.Models;
using Backups.StorageAlgorithms;

namespace Backups.Tools.Creators;

public interface IRestorePointCreator
{
    public IRestorePoint Create(string folderName, IEnumerable<IBackupObject> backupObjects, IStorage storage);
}