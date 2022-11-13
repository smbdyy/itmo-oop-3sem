using Backups.Models;
using Zio;

namespace Backups.Repositories;

public interface IRepository
{
    public UPath BaseDirectory { get; }
    public UPath RestorePointsDirectory { get; }
    public IFileSystem RepositoryFileSystem { get; }

    public void SaveRestorePoint(RestorePoint restorePoint);

    public void CopyDirectoryToDirectory(UPath source, UPath destination)
    {
        if (!RepositoryFileSystem.DirectoryExists(source))
        {
            throw new NotImplementedException();
        }

        UPath dirName = Path.GetFileName(source.FullName);
        RepositoryFileSystem.CreateDirectory(UPath.Combine(destination, dirName));

        foreach (UPath file in RepositoryFileSystem.EnumerateFiles(source))
        {
            RepositoryFileSystem.CopyFile(UPath.Combine(source, file), UPath.Combine(destination, file), true);
        }

        foreach (UPath dir in RepositoryFileSystem.EnumerateFiles(source))
        {
            CopyDirectoryToDirectory(UPath.Combine(source, dir), UPath.Combine(destination, dir));
        }
    }
}