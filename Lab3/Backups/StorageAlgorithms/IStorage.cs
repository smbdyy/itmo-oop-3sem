using Backups.Repositories;

namespace Backups.StorageAlgorithms;

public interface IStorage
{
    public IReadOnlyCollection<IRepositoryObject> GetEntries();
}