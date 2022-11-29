using System.IO.Compression;
using Backups.Repositories;
using Backups.Visitors;

namespace Backups.Archivers;

public class ZipArchiveFile : IRepositoryFile
{
    private ZipArchiveEntry _archiveEntry;

    public ZipArchiveFile(string path, ZipArchiveEntry archiveEntry)
    {
        Path = path;
        _archiveEntry = archiveEntry;
    }

    public string Path { get; }

    public void Accept(IRepositoryVisitor visitor) => visitor.Visit(this);

    public Stream Open()
    {
        return _archiveEntry.Open();
    }
}