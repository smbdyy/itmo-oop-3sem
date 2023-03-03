using Backups.Repositories;

namespace Backups.Archivers;

public interface IStorageArchiver
{
    public string ArchiveExtension { get; }
    public IEnumerable<IRepositoryObject>
        CreateArchive(string archivePath, IRepository repository, IEnumerable<IRepositoryObject> repositoryObjects);
}