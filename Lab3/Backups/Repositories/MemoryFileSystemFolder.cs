using Backups.Visitors;

namespace Backups.Repositories;

public class MemoryFileSystemFolder : IMemoryFileSystemFolder
{
    private readonly List<IMemoryFileSystemObject> _entries;

    public MemoryFileSystemFolder(string path)
    {
        _entries = new List<IMemoryFileSystemObject>();
        Path = path;
    }

    public MemoryFileSystemFolder(string path, IEnumerable<IMemoryFileSystemObject> entries)
    {
        _entries = entries.ToList();
        Path = path;
    }

    public string Path { get; }

    public IReadOnlyCollection<IMemoryFileSystemObject> Entries => _entries;

    public bool IsFile => false;
    public void Accept(IMemoryFileSystemVisitor visitor) => visitor.Visit(this);

    public void AddEntry(IMemoryFileSystemObject entry)
    {
        _entries.Add(entry);
    }
}