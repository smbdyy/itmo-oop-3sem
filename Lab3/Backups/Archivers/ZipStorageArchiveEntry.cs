using System.IO.Compression;

namespace Backups.Archivers;

public class ZipStorageArchiveEntry : IStorageArchiveEntry
{
    private ZipArchiveEntry _entry;

    public ZipStorageArchiveEntry(ZipArchiveEntry entry, string repositoryPath)
    {
        _entry = entry;
        RepositoryPath = repositoryPath;
    }

    public string RepositoryPath { get; }

    public Stream Open()
    {
        return _entry.Open();
    }
}