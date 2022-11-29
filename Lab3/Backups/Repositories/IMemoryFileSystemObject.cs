using Backups.Visitors;

namespace Backups.Repositories;

public interface IMemoryFileSystemObject
{
    public void Accept(IMemoryFileSystemVisitor visitor);
}