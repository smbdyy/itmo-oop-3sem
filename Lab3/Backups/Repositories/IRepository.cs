using Backups.Models;
using Zio;

namespace Backups.Repositories;

public interface IRepository
{
    public UPath BaseDirectory { get; }
    public IFileSystem RepositoryFileSystem { get; }

    public void SaveRestorePoint(RestorePoint restorePoint);
}