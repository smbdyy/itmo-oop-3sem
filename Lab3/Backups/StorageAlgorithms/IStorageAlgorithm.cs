using Backups.Models;

namespace Backups.StorageAlgorithms;

public interface IStorageAlgorithm
{
    public IEnumerable<Storage> MakeStorages(int id, IEnumerable<BackupObject> backupObjects);
}