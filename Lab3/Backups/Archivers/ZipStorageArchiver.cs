using System.IO.Compression;
using Backups.Models;
using Backups.Repositories;
using Backups.Visitors;

namespace Backups.Archivers;

public class ZipStorageArchiver : IStorageArchiver
{
    public IStorageArchive CreateArchive(string name, IRepository repository, BackupObject backupObject)
        => CreateArchive(name, repository, new List<BackupObject> { backupObject });

    public IStorageArchive CreateArchive(string name, IRepository repository, IEnumerable<BackupObject> backupObjects)
    {
        var repositoryObjects = backupObjects.Select(backupObject => repository.GetRepositoryObject(backupObject.Path)).ToList();
        string archivePath = Path.Combine(repository.RestorePointsPath, name + ".zip");
        repository.CreateFile(archivePath);

        using Stream zipFileStream = repository.OpenWrite(archivePath);
        WriteToArchiveStream(zipFileStream, repositoryObjects);
        return new ZipStorageArchive(GetEntriesFromCreatedArchive(zipFileStream, repositoryObjects));
    }

    private static void WriteToArchiveStream(Stream zipFileStream, IEnumerable<IRepositoryObject> repositoryObjects)
    {
        using var archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create);
        var visitor = new ZipArchiverVisitor(archive);
        foreach (IRepositoryObject repositoryObject in repositoryObjects)
        {
            repositoryObject.Accept(visitor);
        }
    }

    private static IEnumerable<ZipStorageArchiveEntry> GetEntriesFromCreatedArchive(
        Stream zipFileStream,
        IEnumerable<IRepositoryObject> repositoryObjects)
    {
        using var archive = new ZipArchive(zipFileStream, ZipArchiveMode.Read);
        var storageArchiveEntries = new List<ZipStorageArchiveEntry>();
        foreach (IRepositoryObject repositoryObject in repositoryObjects)
        {
            ZipArchiveEntry? archiveEntry = GetEntry(archive, repositoryObject.Path);
            storageArchiveEntries.Add(new ZipStorageArchiveEntry(archiveEntry, repositoryObject.Path));
        }

        return storageArchiveEntries;
    }

    private static ZipArchiveEntry GetEntry(ZipArchive archive, string path)
    {
        ZipArchiveEntry? entry = archive.GetEntry(path);
        if (entry is null)
        {
            throw new NotImplementedException();
        }

        return entry;
    }
}