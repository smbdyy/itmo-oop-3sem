using System.IO.Compression;
using Backups.Archivers;
using Backups.Repositories;
using Backups.Tools.Exceptions;

namespace Backups.Visitors;

public class ZipArchiverVisitor : IRepositoryVisitor
{
    private readonly string _archivePath;
    private readonly IRepository _repository;
    private readonly IRepositoryFile _archiveFile;
    private IRepositoryObject? _tree;

    public ZipArchiverVisitor(string archivePath, IRepository repository)
    {
        _archivePath = archivePath;
        _repository = repository;
        IRepositoryObject archiveFile = _repository.GetRepositoryObject(archivePath);
        if (archiveFile is not IRepositoryFile file)
        {
            throw new NotImplementedException();
        }

        _archiveFile = file;
    }

    public IRepositoryObject GetTree()
    {
        if (_tree is null)
        {
            throw new NotImplementedException();
        }

        return _tree;
    }

    public void Visit(IRepositoryFile repositoryFile)
    {
        using Stream zipFileStream = _repository.OpenWrite(_archivePath);
        using var archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create);
        ZipArchiveEntry entry = archive.CreateEntry(repositoryFile.Path);
        using Stream entryStream = entry.Open();
        using Stream fileStream = repositoryFile.Open();
        fileStream.CopyTo(entryStream);
        _tree = new ZipArchiveFile(repositoryFile.Path, _archiveFile);
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

        foreach (IRepositoryObject folderEntry in repositoryFolder.Entries)
        {
            var visitor = new ZipArchiverVisitor(_archivePath, _repository);
            folderEntry.Accept(visitor);
            _tree = visitor.GetTree();
        }
    }
}