using System.IO.Compression;
using Backups.Archivers;
using Backups.Repositories;
using Backups.Tools.Exceptions;

namespace Backups.Visitors;

public class ZipArchiverVisitor : IRepositoryVisitor
{
    private string _archivePath;
    private IRepository _repository;
    private IRepositoryObject? _composite;

    public ZipArchiverVisitor(string archivePath, IRepository repository)
    {
        _archivePath = archivePath;
        _repository = repository;
    }

    public IRepositoryObject GetComposite()
    {
        if (_composite is null)
        {
            throw ArchiverException.CompositeNotBuilt();
        }

        return _composite;
    }

    public void Visit(IRepositoryFile repositoryFile)
    {
        using Stream zipFileStream = _repository.OpenWrite(_archivePath);
        using var archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create);
        ZipArchiveEntry entry = archive.CreateEntry(repositoryFile.Path);
        using (Stream entryStream = entry.Open())
        {
            Stream fileStream = repositoryFile.Open();
            fileStream.CopyTo(entryStream);
            fileStream.Dispose();
        }

        _composite = new ZipArchiveFile(repositoryFile.Path, entry);
    }

    public void Visit(IRepositoryFolder repositoryFolder)
    {
        using (Stream zipFileStream = _repository.OpenWrite(_archivePath))
        {
            using (var archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
            {
                archive.CreateEntry(repositoryFolder.Path + Path.DirectorySeparatorChar);
            }
        }

        var compositeEntries = new List<IRepositoryObject>();
        foreach (IRepositoryObject folderEntry in repositoryFolder.Entries)
        {
            var visitor = new ZipArchiverVisitor(_archivePath, _repository);
            folderEntry.Accept(visitor);
            compositeEntries.Add(visitor.GetComposite());
        }

        _composite = new ZipArchiveFolder(repositoryFolder.Path, compositeEntries);
    }
}