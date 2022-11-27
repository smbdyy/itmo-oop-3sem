namespace Backups.Repositories;

public interface IRepositoryFile : IRepositoryObject
{
    public Stream Open();
}