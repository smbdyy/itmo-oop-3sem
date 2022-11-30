using Backups.Repositories;

namespace Backups.Visitors;

public class InMemoryRepositoryFileSystemVisitor : IMemoryFileSystemVisitor
{
    private IRepositoryObject? _composite;

    public void Visit(IMemoryFileSystemFile file)
    {
        _composite = new InMemoryRepositoryFile(file);
    }

    public void Visit(IMemoryFileSystemFolder folder)
    {
        var entries = new List<IRepositoryObject>();

        foreach (IMemoryFileSystemObject entry in folder.Entries)
        {
            var visitor = new InMemoryRepositoryFileSystemVisitor();
            entry.Accept(visitor);
            entries.Add(visitor.GetComposite());
        }

        _composite = new InMemoryRepositoryFolder(folder.Path, entries);
    }

    public IRepositoryObject GetComposite()
    {
        if (_composite is null)
        {
            throw new NotImplementedException();
        }

        return _composite;
    }
}