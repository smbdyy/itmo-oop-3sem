using Backups.Visitors;

namespace Backups.Repositories;

public class InMemoryRepositoryFolder : IRepositoryFolder
{
    private List<IRepositoryObject> _entries;

    public InMemoryRepositoryFolder(string path, IEnumerable<IRepositoryObject> entries)
    {
        Path = path;
        _entries = entries.ToList();
    }

    public string Path { get; }
    public IReadOnlyCollection<IRepositoryObject> Entries => _entries;
    public void Accept(IRepositoryVisitor visitor) => visitor.Visit(this);
}