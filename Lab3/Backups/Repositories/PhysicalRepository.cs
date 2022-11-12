using Backups.Models;
using Ionic.Zip;
using Zio;
using Zio.FileSystems;

namespace Backups.Repositories;

public class PhysicalRepository : IRepository
{
    public PhysicalRepository(UPath baseDirectory, UPath restorePointsDirectory)
    {
        RepositoryFileSystem.CreateDirectory(baseDirectory);
        RepositoryFileSystem.CreateDirectory(restorePointsDirectory);

        BaseDirectory = baseDirectory;
        RestorePointsDirectory = restorePointsDirectory;
    }

    public UPath BaseDirectory { get; }
    public UPath RestorePointsDirectory { get; }
    public IFileSystem RepositoryFileSystem { get; } = new PhysicalFileSystem();

    public void SaveRestorePoint(RestorePoint restorePoint)
    {
        foreach (Storage storage in restorePoint.Storages)
        {
            string zipFilePath = UPath.Combine(RestorePointsDirectory, storage.Name).FullName;
            var zip = new ZipFile();
            foreach (BackupObject backupObject in storage.BackupObjects)
            {
                zip.AddItem(backupObject.ObjectPath.FullName, string.Empty);
            }

            zip.Save(zipFilePath);
        }
    }
}