using Backups.Models;
using Backups.Repositories;
using Backups.RestorePoints;

namespace Backups.Services;

public interface IBackupTask
{
    public string Name { get; }
    public string ArchiveExtension { get; }
    public IRepository Repository { get; }
    public IReadOnlyCollection<IRestorePoint> RestorePoints { get; }
    public IReadOnlyCollection<IBackupObject> BackupObjects { get; }
    public void CreateRestorePoint();
    public void AddBackupObject(IBackupObject backupObject);
    public void RemoveBackupObject(IBackupObject backupObject);
}