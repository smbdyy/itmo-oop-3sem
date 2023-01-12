using Backups.Archivers;
using Backups.Models;
using Backups.Repositories;

namespace Backups.StorageAlgorithms;

public class SingleStorageAlgorithm : IStorageAlgorithm
{
    public IStorage MakeStorage(
        int id, IRepository repository, IStorageArchiver storageArchiver, IEnumerable<IRepositoryObject> objects)
    {
        return new Storage(storageArchiver.CreateArchive($"RestorePoint_{id}", repository, objects));
    }
}