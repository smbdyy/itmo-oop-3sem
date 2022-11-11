using Backups.Models;

namespace Backups.Interfaces;

public interface IStorageAlgorithm
{
    public List<Storage> MakeStorages(IEnumerable<BackupObject> backupObjects);
}