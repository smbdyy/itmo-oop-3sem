using Backups.Interfaces;

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

        if (!Repository.FileSystem.DirectoryExists(backupObject.ObjectPath) &&
            !Repository.FileSystem.FileExists(backupObject.ObjectPath))
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

    public void CreateRestorePoint(string name)
    {
        List<Storage> storages = StorageAlgorithm.MakeStorages(_backupObjects);
        int id = _restorePoints.Count == 0 ? 0 : _restorePoints.Last().Id + 1;

        var restorePoint = new RestorePoint(id, name, storages);
        Repository.SaveRestorePoint(restorePoint);
        _restorePoints.Add(restorePoint);
    }
}