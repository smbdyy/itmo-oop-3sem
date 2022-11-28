using Backups.Repositories;
using Backups.Tools.Exceptions;

namespace Backups.Archivers;

public class ZipStorageArchive : IStorageArchive
{
    private List<IRepositoryObject> _entries;

    public ZipStorageArchive(string name, IEnumerable<IRepositoryObject> entries)
    {
        if (name == string.Empty)
        {
            throw BackupsArgumentException.EmptyPathString();
        }

        _entries = entries.ToList();
        Name = name;
    }

    public string Name { get; }

    public IReadOnlyCollection<IRepositoryObject> GetEntries() => _entries;
}