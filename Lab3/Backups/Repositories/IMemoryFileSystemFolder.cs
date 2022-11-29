namespace Backups.Repositories;

public interface IMemoryFileSystemFolder : IMemoryFileSystemObject
{
    public IReadOnlyCollection<IMemoryFileSystemObject> Entries { get; }
    public void AddEntry(IMemoryFileSystemObject entry);
}