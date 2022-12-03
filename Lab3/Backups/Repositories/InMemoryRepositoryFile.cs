using Backups.Visitors;
using Zio;

namespace Backups.Repositories;

public class InMemoryRepositoryFile : IRepositoryFile
{
    private IFileSystem _fileSystem;

    public InMemoryRepositoryFile(string path, IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
        Path = path;
    }

    public string Path { get; }
    public void Accept(IRepositoryVisitor visitor) => visitor.Visit(this);

    public Stream Open()
    {
        return _fileSystem.OpenFile(Path, FileMode.Open, FileAccess.Read);
    }
}