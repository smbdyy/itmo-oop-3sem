using Backups.Repositories;

namespace Backups.StorageAlgorithms;

public interface IStorageAlgorithm
{
    IStorage MakeStorage(IEnumerable<IRepositoryObject> repositoryObjects);
}