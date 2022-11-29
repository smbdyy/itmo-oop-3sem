using Backups.Visitors;

namespace Backups.Repositories;

public class InMemoryRepositoryFile : IRepositoryFile
{
    private readonly IMemoryFileSystemFile _file;

    public InMemoryRepositoryFile(IMemoryFileSystemFile file)
    {
        _file = file;
    }

    public string Path => _file.Path;
    public void Accept(IRepositoryVisitor visitor) => visitor.Visit(this);
    public Stream Open() => _file.OpenRead();
}