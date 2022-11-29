using Backups.Visitors;

namespace Backups.Repositories;

public class MemoryFileSystemFolder : IMemoryFileSystemFolder
{
    private readonly List<IMemoryFileSystemObject> _entries;

    public MemoryFileSystemFolder()
    {
        _entries = new List<IMemoryFileSystemObject>();
    }

    public MemoryFileSystemFolder(IEnumerable<IMemoryFileSystemObject> entries)
    {
        _entries = entries.ToList();
    }

    public IReadOnlyCollection<IMemoryFileSystemObject> Entries => _entries;

    public void Accept(IMemoryFileSystemVisitor visitor) => visitor.Visit(this);

    public void AddEntry(IMemoryFileSystemObject entry)
    {
        _entries.Add(entry);
    }
}