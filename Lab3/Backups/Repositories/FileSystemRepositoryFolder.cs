namespace Backups.Repositories;

public class FileSystemRepositoryFolder : RepositoryFolder
{
    private readonly List<IRepositoryObject> _entries;

    public FileSystemRepositoryFolder()
    {
        _entries = new List<IRepositoryObject>();
    }

    public FileSystemRepositoryFolder(IEnumerable<IRepositoryObject> entries)
    {
        _entries = entries.ToList();
    }

    public override IReadOnlyCollection<IRepositoryObject> Entries => _entries;
}