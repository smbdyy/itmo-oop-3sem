using System.IO.Compression;
using Backups.Archivers;
using Backups.Repositories;

namespace Backups.Visitors;

public class ZipArchiverVisitor : IVisitor
{
    private ZipArchive _archive;

    public ZipArchiverVisitor(ZipArchive archive)
    {
        _archive = archive;
    }

    public void Visit(IRepositoryFile repositoryFile)
    {
        ZipArchiveEntry entry = _archive.CreateEntry(repositoryFile.Path);
        using Stream entryStream = entry.Open();
        repositoryFile.Open().CopyTo(entryStream);
    }

    public void Visit(IRepositoryFolder repositoryFolder)
    {
        _archive.CreateEntry(repositoryFolder.Path + Path.DirectorySeparatorChar);
        foreach (IRepositoryObject folderEntry in repositoryFolder.Entries)
        {
            folderEntry.Accept(this);
        }
    }
}