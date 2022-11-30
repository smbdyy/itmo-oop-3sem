using Backups.Repositories;
using Backups.Tools.Utils;

namespace Backups.Visitors;

public class MemoryFileFinderVisitor : IMemoryFileSystemVisitor
{
    private IMemoryFileSystemFile? _file;
    private string _path;

    public MemoryFileFinderVisitor(string path)
    {
        _path = path;
    }

    public void Visit(IMemoryFileSystemFile file)
    {
        if (file.Path == _path)
        {
            _file = file;
        }
    }

    public void Visit(IMemoryFileSystemFolder folder)
    {
        foreach (IMemoryFileSystemObject entry in folder.Entries)
        {
            entry.Accept(this);
            if (this._file is not null)
            {
                return;
            }
        }
    }
}