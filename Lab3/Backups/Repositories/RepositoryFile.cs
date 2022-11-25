namespace Backups.Repositories;

public abstract class RepositoryFile : IRepositoryObject
{
    public bool IsFile => true;
    public abstract Stream Open();
}