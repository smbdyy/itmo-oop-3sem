using System.IO.Compression;
using Backups.Repositories;
using Backups.Tools.Exceptions;
using Backups.Visitors;

namespace Backups.Archivers;

public class ZipArchiveFile : IRepositoryFile
{
    private readonly IRepositoryFile _archiveFile;

    public ZipArchiveFile(string path, IRepositoryFile archiveFile)
    {
        Path = path;
        _archiveFile = archiveFile;
    }

    public string Path { get; }

    public void Accept(IRepositoryVisitor visitor) => visitor.Visit(this);

    public Stream Open()
    {
        var stream = new MemoryStream();
        using Stream archiveFileStream = _archiveFile.Open();
        using var archive = new ZipArchive(archiveFileStream, ZipArchiveMode.Read);
        ZipArchiveEntry? entry = archive.GetEntry(Path);
        if (entry is null)
        {
            throw RepositoryException.FileNotFound(Path);
        }

        using Stream entryStream = entry.Open();
        entryStream.CopyTo(stream);

        return stream;
    }
}