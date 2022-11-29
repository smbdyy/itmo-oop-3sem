using Backups.Visitors;

namespace Backups.Repositories;

public class FileSystemRepositoryFile : IRepositoryFile
{
    private readonly FileSystemRepository _repository;

    public FileSystemRepositoryFile(string path, FileSystemRepository repository)
    {
        Path = path;
        _repository = repository;
    }

    public string Path { get; }
    public void Accept(IRepositoryVisitor visitor) => visitor.Visit(this);

    public Stream Open()
    {
        return _repository.OpenRead(Path);
    }
}