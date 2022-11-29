using Backups.Repositories;

namespace Backups.Visitors;

public interface IMemoryFileSystemVisitor
{
    public void Visit(IMemoryFileSystemFile file);
    public void Visit(IMemoryFileSystemFolder folder);
}