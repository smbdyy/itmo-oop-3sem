namespace Backups.Repositories;

public interface IRepositoryFolder : IRepositoryObject
{
    public IReadOnlyCollection<IRepositoryObject> Entries { get; }
}