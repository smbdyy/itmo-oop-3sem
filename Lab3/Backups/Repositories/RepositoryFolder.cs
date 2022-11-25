namespace Backups.Repositories;

public abstract class RepositoryFolder : IRepositoryObject
{
    public bool IsFile => false;
    public abstract IReadOnlyCollection<IRepositoryObject> Entries { get; }
}