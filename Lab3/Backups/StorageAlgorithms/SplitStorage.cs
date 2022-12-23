using Backups.Archivers;
using Backups.Repositories;

namespace Backups.StorageAlgorithms;

public class SplitStorage : IStorage
{
    private readonly List<IStorageArchive> _archives;

    public SplitStorage(IEnumerable<IStorageArchive> archives)
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

    public IEnumerable<string> GetArchiveNames()
    {
        return _archives.Select(archive => archive.Name);
    }
}