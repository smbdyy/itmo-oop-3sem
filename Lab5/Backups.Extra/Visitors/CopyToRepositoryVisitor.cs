using Backups.Repositories;
using Backups.Visitors;

namespace Backups.Extra.Visitors;

public class CopyToRepositoryVisitor : IRepositoryVisitor
{
    private readonly IRepository _repository;

    public CopyToRepositoryVisitor(IRepository repository)
    {
        _repository = repository;
    }

    public void Visit(IRepositoryFolder repositoryFolder)
    {
        if (!_repository.DirectoryExists(repositoryFolder.Path))
        {
            _repository.CreateDirectory(repositoryFolder.Path);
        }

        foreach (IRepositoryObject repositoryObject in repositoryFolder.Entries)
        {
            repositoryObject.Accept(this);
        }
    }

    public void Visit(IRepositoryFile repositoryFile)
    {
        if (!_repository.FileExists(repositoryFile.Path))
        {
            _repository.CreateFile(repositoryFile.Path);
        }

        using Stream createdFileStream = _repository.OpenWrite(repositoryFile.Path);
        using Stream repositoryFileStream = repositoryFile.Open();
        repositoryFileStream.CopyTo(createdFileStream);
    }
}