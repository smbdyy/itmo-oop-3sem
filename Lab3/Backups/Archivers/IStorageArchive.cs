using Backups.Repositories;

namespace Backups.Archivers;

public interface IStorageArchive
{
    public string Name { get; }
    public IReadOnlyCollection<IRepositoryObject> GetEntries();
}