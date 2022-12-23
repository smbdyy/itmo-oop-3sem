using Backups.Archivers;
using Backups.Models;
using Backups.Repositories;

namespace Backups.StorageAlgorithms;

public interface IStorageAlgorithm
{
    IStorage MakeStorage(int id, IRepository repository, IStorageArchiver storageArchiver, IEnumerable<IRepositoryObject> objects);
}