using Backups.Repositories;
using Backups.Visitors;

namespace Backups.Archivers;

public class ZipStorageArchiver : IStorageArchiver
{
    public string ArchiveExtension => ".zip";
    public IEnumerable<IRepositoryObject> CreateArchive(
        string archivePath,
        IRepository repository,
        IEnumerable<IRepositoryObject> repositoryObjects)
    {
        var archiveEntries = new List<IRepositoryObject>();

        foreach (IRepositoryObject repositoryObject in repositoryObjects)
        {
            var visitor = new ZipArchiverVisitor(archivePath, repository);
            repositoryObject.Accept(visitor);
            archiveEntries.Add(visitor.GetTree());
        }

        return archiveEntries;
    }
}