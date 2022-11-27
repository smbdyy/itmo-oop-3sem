namespace Backups.Archivers;

public interface IStorageArchiveEntry
{
    public string RepositoryPath { get; }
    public Stream Open();
}
