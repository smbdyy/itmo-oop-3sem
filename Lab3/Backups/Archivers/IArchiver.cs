using Backups.Repositories;

namespace Backups.Archivers;

public interface IArchiver
{
    public void AddArchive(IRepositoryObject archiveFile, IEnumerable<IRepositoryObject> repositoryObjects);
}