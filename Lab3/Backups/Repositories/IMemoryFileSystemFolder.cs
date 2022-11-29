namespace Backups.Repositories;

public interface IMemoryFileSystemFolder : IMemoryFileSystemObject
{
    public IEnumerable<IMemoryFileSystemObject> Entries { get; }
    public void AddEntry(IMemoryFileSystemObject entry);
}