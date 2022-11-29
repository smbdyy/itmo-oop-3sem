using System.IO.Compression;
using Backups.Archivers;
using Backups.Repositories;
using Backups.Tools.Exceptions;

namespace Backups.Visitors;

public class ZipArchiverVisitor : IRepositoryVisitor
{
    private ZipArchive _archive;
    private IRepositoryObject? _composite;

    public ZipArchiverVisitor(ZipArchive archive)
    {
        _archive = archive;
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
        ZipArchiveEntry entry = _archive.CreateEntry(repositoryFile.Path);
        using (Stream entryStream = entry.Open())
        {
            repositoryFile.Open().CopyTo(entryStream);
        }

        _composite = new ZipArchiveFile(repositoryFile.Path, entry);
    }

    public void Visit(IRepositoryFolder repositoryFolder)
    {
        _archive.CreateEntry(repositoryFolder.Path + Path.DirectorySeparatorChar);
        var compositeEntries = new List<IRepositoryObject>();
        foreach (IRepositoryObject folderEntry in repositoryFolder.Entries)
        {
            var visitor = new ZipArchiverVisitor(_archive);
            folderEntry.Accept(visitor);
            compositeEntries.Add(visitor.GetComposite());
        }

        _composite = new ZipArchiveFolder(repositoryFolder.Path, compositeEntries);
    }
}