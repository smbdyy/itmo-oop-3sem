using Backups.Archivers;
using Backups.Repositories;

namespace Backups.StorageAlgorithms;

public interface IStorageAlgorithm
{
    IStorage MakeStorage(
        int id, string path, IRepository repository, IStorageArchiver storageArchiver, IEnumerable<IRepositoryObject> objects);
}