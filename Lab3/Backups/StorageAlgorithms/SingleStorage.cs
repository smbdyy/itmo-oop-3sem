using Backups.Archivers;
using Backups.Repositories;

namespace Backups.StorageAlgorithms;

public class SingleStorage : IStorage
{
    private readonly List<IStorageArchive> _archives;

    public SingleStorage(IEnumerable<IStorageArchive> archives)
    {
        _archives = archives.ToList();
    }

    public IReadOnlyCollection<IRepositoryObject> GetEntries()
    {
        var entries = new List<IRepositoryObject>();
        foreach (IStorageArchive archive in _archives)
        {
            entries.AddRange(archive.GetEntries());
        }

        return entries;
    }
}