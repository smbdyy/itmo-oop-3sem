using Backups.Models;
using Backups.StorageAlgorithms;

namespace Backups.Tools.Creators;

public interface IRestorePointCreator
{
    public IRestorePoint Create(IEnumerable<IBackupObject> backupObjects, IStorage storage);
}