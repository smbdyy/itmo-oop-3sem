using System.IO.Compression;
using Backups.Models;
using Backups.Repositories;
using Backups.Visitors;

namespace Backups.Archivers;

public class ZipStorageArchiver : IStorageArchiver
{
    public string ArchiveExtension => ".zip";

    public IStorageArchive CreateArchive(string name, IRepository repository, IRepositoryObject repositoryObject)
        => CreateArchive(name, repository, new List<IRepositoryObject> { repositoryObject });

    public IStorageArchive CreateArchive(string name, IRepository repository, IEnumerable<IRepositoryObject> repositoryObjects)
    {
        string archivePath = Path.Combine(repository.RestorePointsPath, name + ArchiveExtension);
        repository.CreateFile(archivePath);

        IEnumerable<IRepositoryObject> compositeEntries = CreateArchiveCompositeEntries(archivePath, repository, repositoryObjects);
        return new ZipStorageArchive(name + ArchiveExtension, compositeEntries);
    }

    private static IEnumerable<IRepositoryObject> CreateArchiveCompositeEntries(
        string archivePath,
        IRepository repository,
        IEnumerable<IRepositoryObject> repositoryObjects)
    {
        var entries = new List<IRepositoryObject>();
        foreach (IRepositoryObject repositoryObject in repositoryObjects)
        {
            var visitor = new ZipArchiverVisitor(archivePath, repository);
            repositoryObject.Accept(visitor);
            entries.Add(visitor.GetComposite());
        }

        return entries;
    }
}