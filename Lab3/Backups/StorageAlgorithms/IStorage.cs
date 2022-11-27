using Backups.Repositories;

namespace Backups.StorageAlgorithms;

public interface IStorage
{
    public IEnumerable<IRepositoryObject> GetEntries();
}