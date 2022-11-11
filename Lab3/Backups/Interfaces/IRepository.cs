using Backups.Models;
using Zio;

namespace Backups.Interfaces;

public interface IRepository
{
    public UPath BaseDirectory { get; }
    public IFileSystem FileSystem { get; }

    public void SaveRestorePoint(RestorePoint restorePoint);
}