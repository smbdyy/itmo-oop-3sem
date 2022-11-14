using Backups.Models;

namespace Backups.StorageAlgorithms;

public class SingleStorageAlgorithm : IStorageAlgorithm
{
    public IEnumerable<Storage> MakeStorages(int id, IEnumerable<BackupObject> backupObjects)
    {
        string name = $"Storage_{id}.zip";
        var storage = new Storage(name, backupObjects);
        return new List<Storage> { storage };
    }
}