using Backups.Repositories;
using Backups.Visitors;

namespace Backups.Archivers;

public class ZipArchiveFolder : IRepositoryFolder
{
    private readonly List<IRepositoryObject> _entries;

    public ZipArchiveFolder(string path, IEnumerable<IRepositoryObject> entries)
    {
        _entries = entries.ToList();
        Path = path;
    }

    public IReadOnlyCollection<IRepositoryObject> Entries => _entries;
    public string Path { get; }
    public void Accept(IRepositoryVisitor visitor) => visitor.Visit(this);
}