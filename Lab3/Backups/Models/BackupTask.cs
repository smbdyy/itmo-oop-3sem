using Backups.Repositories;
using Backups.StorageAlgorithms;

namespace Backups.Models;

public class BackupTask
{
    private List<BackupObject> _backupObjects = new ();
    private List<RestorePoint> _restorePoints = new ();

    public BackupTask(IRepository repository, IStorageAlgorithm storageAlgorithm)
    {
        Repository = repository;
        StorageAlgorithm = storageAlgorithm;
    }

    public IRepository Repository { get; }
    public IStorageAlgorithm StorageAlgorithm { get; }
    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;
    public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints;

    public void AddBackupObject(BackupObject backupObject)
    {
        if (_backupObjects.Any(obj => obj.ObjectPath == backupObject.ObjectPath))
        {
            throw new NotImplementedException();
        }

        if (!Repository.RepositoryFileSystem.DirectoryExists(backupObject.ObjectPath) &&
            !Repository.RepositoryFileSystem.FileExists(backupObject.ObjectPath))
        {
            throw new NotImplementedException();
        }

        _backupObjects.Add(backupObject);
    }

    public void DeleteBackupObject(BackupObject backupObject)
    {
        if (!_backupObjects.Contains(backupObject))
        {
            throw new NotImplementedException();
        }

        _backupObjects.Remove(backupObject);
    }

    public void CreateRestorePoint()
    {
        int id = _restorePoints.Count == 0 ? 0 : _restorePoints.Last().Id + 1;
        List<Storage> storages = StorageAlgorithm.MakeStorages(id, _backupObjects);

        var restorePoint = new RestorePoint(id, storages);
        Repository.SaveRestorePoint(restorePoint);
        _restorePoints.Add(restorePoint);
    }
}