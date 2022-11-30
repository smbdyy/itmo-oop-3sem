using Backups.Visitors;

namespace Backups.Repositories;

public interface IMemoryFileSystemObject
{
    public string Path { get; }
    public bool IsFile { get; }
    public void Accept(IMemoryFileSystemVisitor visitor);
}