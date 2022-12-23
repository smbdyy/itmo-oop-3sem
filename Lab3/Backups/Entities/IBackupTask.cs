using Backups.Models;
using Backups.Repositories;

namespace Backups.Entities;

public interface IBackupTask
{
    public string Name { get; }
    public IRepository Repository { get; }
    public IReadOnlyCollection<IRestorePoint> RestorePoints { get; }
    public IReadOnlyCollection<IBackupObject> BackupObjects { get; }
    public void CreateRestorePoint();
    public void DeleteRestorePoint(IRestorePoint restorePoint);
    public void AddBackupObject(IBackupObject backupObject);
    public void RemoveBackupObject(IBackupObject backupObject);
}