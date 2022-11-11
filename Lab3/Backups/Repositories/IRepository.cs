using Backups.Models;
using Zio;

namespace Backups.Repositories;

public interface IRepository
{
    public UPath BaseDirectory { get; }
    public IFileSystem FileSystem { get; }

    public void SaveRestorePoint(RestorePoint restorePoint);
}