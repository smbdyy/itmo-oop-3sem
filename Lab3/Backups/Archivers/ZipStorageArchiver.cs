using System.IO.Compression;
using Backups.Models;
using Backups.Repositories;
using Backups.Visitors;

namespace Backups.Archivers;

public class ZipStorageArchiver : IStorageArchiver
{
    public string ArchiveExtension => ".zip";

    public IStorageArchive CreateArchive(string name, IRepository repository, BackupObject backupObject)
        => CreateArchive(name, repository, new List<BackupObject> { backupObject });

    public IStorageArchive CreateArchive(string name, IRepository repository, IEnumerable<BackupObject> backupObjects)
    {
        var repositoryObjects = backupObjects.Select(backupObject => repository.GetRepositoryObject(backupObject.Path)).ToList();
        string archivePath = Path.Combine(repository.RestorePointsPath, name + ArchiveExtension);
        repository.CreateFile(archivePath);

        using Stream zipFileStream = repository.OpenWrite(archivePath);
        IEnumerable<IRepositoryObject> compositeEntries = CreateArchiveCompositeEntries(zipFileStream, repositoryObjects);
        return new ZipStorageArchive(name + ArchiveExtension, compositeEntries);
    }

    private static IEnumerable<IRepositoryObject> CreateArchiveCompositeEntries(Stream zipFileStream, IEnumerable<IRepositoryObject> repositoryObjects)
    {
        var entries = new List<IRepositoryObject>();
        using var archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create);
        foreach (IRepositoryObject repositoryObject in repositoryObjects)
        {
            var visitor = new ZipArchiverVisitor(archive);
            repositoryObject.Accept(visitor);
            entries.Add(visitor.GetComposite());
        }

        return entries;
    }
}