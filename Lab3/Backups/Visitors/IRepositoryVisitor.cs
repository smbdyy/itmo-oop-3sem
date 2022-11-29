using Backups.Repositories;

namespace Backups.Visitors;

public interface IRepositoryVisitor
{
    public void Visit(IRepositoryFile repositoryFile);
    public void Visit(IRepositoryFolder repositoryFolder);
}