using Backups.Archivers;
using Backups.Repositories;

namespace Backups.StorageAlgorithms;

public class SingleStorage : IStorage
{
    private readonly IStorageArchive _archive;

    public SingleStorage(IStorageArchive archive)
    {
        _archive = archive;
    }

    public IReadOnlyCollection<IRepositoryObject> GetEntries() => _archive.GetEntries();
}