using Backups.Models;

namespace Backups.StorageAlgorithms;

public interface IStorageAlgorithm
{
    public List<Storage> MakeStorages(int id, IEnumerable<BackupObject> backupObjects);
}