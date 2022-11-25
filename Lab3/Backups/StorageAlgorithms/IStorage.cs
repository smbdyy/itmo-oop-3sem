using Backups.Repositories;

namespace Backups.StorageAlgorithms;

public interface IStorage
{
    IEnumerable<IRepositoryObject> GetEntries();
}