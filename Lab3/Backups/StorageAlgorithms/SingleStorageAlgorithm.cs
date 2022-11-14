using Backups.Models;

namespace Backups.StorageAlgorithms;

public class SingleStorageAlgorithm : IStorageAlgorithm
{
    public IEnumerable<Storage> MakeStorages(int id, IEnumerable<BackupObject> backupObjects)
    {
        IEnumerable<BackupObject> backupObjectsList = backupObjects.ToList();
        if (!backupObjectsList.Any())
        {
            throw new NotImplementedException();
        }

        string name = $"Storage_{id}.zip";
        var storage = new Storage(name, backupObjectsList);
        return new List<Storage> { storage };
    }
}