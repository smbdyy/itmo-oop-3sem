using Backups.Visitors;

namespace Backups.Repositories;

public interface IRepositoryObject
{
    public string Path { get; }
    public void Accept(IVisitor visitor);
}