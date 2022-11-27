using Backups.Repositories;
using Backups.Visitors;

namespace Backups.Archivers;

public class ZipArchiveFolder : IRepositoryFolder
{
    private List<IRepositoryObject> _entries;

    public ZipArchiveFolder(string path, IEnumerable<IRepositoryObject> entries)
    {
        _entries = entries.ToList();
        Path = path;
    }

    public IReadOnlyCollection<IRepositoryObject> Entries => _entries;
    public string Path { get; }
    public void Accept(IVisitor visitor) => visitor.Visit(this);
}