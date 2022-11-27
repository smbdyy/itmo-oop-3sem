using Backups.Archivers;
using Backups.Models;
using Backups.Repositories;

namespace Backups.StorageAlgorithms;

public interface IStorageAlgorithm
{
    IStorage MakeStorage(IRepository repository, IStorageArchiver storageArchiver, IEnumerable<BackupObject> objects);
}