using Backups.Visitors;

namespace Backups.Repositories;

public class FileSystemRepositoryFolder : IRepositoryFolder
{
    private readonly List<IRepositoryObject> _entries;

    public FileSystemRepositoryFolder(string path, IEnumerable<IRepositoryObject> entries)
    {
        _entries = entries.ToList();
        Path = path;
    }

    public IReadOnlyCollection<IRepositoryObject> Entries => _entries;
    public string Path { get; }

    public void Accept(IVisitor visitor) => visitor.Visit(this);
}