using Backups.Models;
using ArgumentException = Backups.Exceptions.ArgumentException;

namespace Backups.StorageAlgorithms;

public class SingleStorageAlgorithm : IStorageAlgorithm
{
    public IEnumerable<Storage> MakeStorages(int id, IEnumerable<BackupObject> backupObjects)
    {
        IEnumerable<BackupObject> backupObjectsList = backupObjects.ToList();
        if (!backupObjectsList.Any())
        {
            throw ArgumentException.EmptyList();
        }

        string name = $"Storage_{id}.zip";
        var storage = new Storage(name, backupObjectsList);
        return new List<Storage> { storage };
    }
}