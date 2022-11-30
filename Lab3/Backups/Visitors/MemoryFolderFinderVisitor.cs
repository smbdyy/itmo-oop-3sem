using Backups.Repositories;

namespace Backups.Visitors;

public class MemoryFolderFinderVisitor : IMemoryFileSystemVisitor
{
    private IMemoryFileSystemFolder? _folder;
    private string _path;

    public MemoryFolderFinderVisitor(string path)
    {
        _path = path;
    }

    public void Visit(IMemoryFileSystemFile file) { }

    public void Visit(IMemoryFileSystemFolder folder)
    {
        if (folder.Path == _path)
        {
            _folder = folder;
        }

        foreach (IMemoryFileSystemObject entry in folder.Entries)
        {
            entry.Accept(this);
            if (_folder is not null)
            {
                return;
            }
        }
    }

    public IMemoryFileSystemFolder? GetFound() => _folder;

    public void Reset()
    {
        _folder = null;
    }
}