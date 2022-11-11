using Backups.Models;

namespace Backups.StorageAlgorithms;

public interface IStorageAlgorithm
{
    public List<Storage> MakeStorages(IEnumerable<BackupObject> backupObjects);
}