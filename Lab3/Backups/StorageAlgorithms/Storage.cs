using Backups.Repositories;

namespace Backups.StorageAlgorithms;

public class Storage : IStorage
{
    private readonly List<IRepositoryObject> _entries;

    public Storage(IEnumerable<IRepositoryObject> entries)
    {
        _entries = entries.ToList();
    }

    public IReadOnlyCollection<IRepositoryObject> GetEntries() => _entries;
}