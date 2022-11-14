using Backups.Models;
using Zio;
using Zio.FileSystems;

namespace Backups.Repositories;

public class InMemoryRepository : IRepository
{
    public InMemoryRepository(UPath baseDirectory, UPath restorePointsDirectory)
    {
        RepositoryFileSystem.CreateDirectory(baseDirectory);
        RepositoryFileSystem.CreateDirectory(restorePointsDirectory);

        BaseDirectory = baseDirectory;
        RestorePointsDirectory = restorePointsDirectory;
    }

    public UPath BaseDirectory { get; }
    public UPath RestorePointsDirectory { get; }
    public IFileSystem RepositoryFileSystem { get; } = new MemoryFileSystem();

    public void SaveRestorePoint(RestorePoint restorePoint)
    {
        foreach (Storage storage in restorePoint.Storages)
        {
            var storageFullPath = UPath.Combine(RestorePointsDirectory, storage.Name);
            RepositoryFileSystem.CreateDirectory(storageFullPath);
            foreach (BackupObject backupObject in storage.BackupObjects)
            {
                var objectFullPath = UPath.Combine(BaseDirectory, backupObject.RelativePath);
                if (RepositoryFileSystem.DirectoryExists(objectFullPath))
                {
                    var thisAsIRepository = (IRepository)this;
                    thisAsIRepository.CopyDirectoryToDirectory(objectFullPath, storageFullPath);
                }
                else
                {
                    UPath fileName = Path.GetFileName(objectFullPath.FullName);
                    RepositoryFileSystem.CopyFile(objectFullPath, UPath.Combine(storageFullPath, fileName), true);
                }
            }
        }
    }
}