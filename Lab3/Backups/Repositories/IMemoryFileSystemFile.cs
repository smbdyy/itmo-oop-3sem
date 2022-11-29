namespace Backups.Repositories;

public interface IMemoryFileSystemFile : IMemoryFileSystemObject
{
    public Stream OpenRead();
    public Stream OpenWrite();
}