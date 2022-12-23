using Backups.Repositories;

namespace Backups.StorageAlgorithms;

public interface IStorage
{
    public IReadOnlyCollection<IRepositoryObject> GetEntries();
    public IEnumerable<string> GetArchiveNames();
}