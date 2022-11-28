using Backups.Archivers;
using Backups.Models;
using Backups.Repositories;

namespace Backups.StorageAlgorithms;

public class SplitStorageAlgorithm : IStorageAlgorithm
{
    public IStorage MakeStorage(int id, IRepository repository, IStorageArchiver storageArchiver, IEnumerable<IBackupObject> objects)
    {
        var archives = new List<IStorageArchive>();
        foreach (IBackupObject backupObject in objects)
        {
            string archiveName = $"{Path.GetFileName(backupObject.Path)}({id})";
            archives.Add(storageArchiver.CreateArchive(archiveName, repository, backupObject));
        }

        return new SplitStorage(archives);
    }
}