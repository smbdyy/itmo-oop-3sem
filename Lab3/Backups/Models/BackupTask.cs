using Backups.Exceptions;
using Backups.Repositories;
using Backups.StorageAlgorithms;
using Zio;
using ArgumentException = Backups.Exceptions.ArgumentException;

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
        if (_backupObjects.Any(obj => obj.RelativePath == backupObject.RelativePath))
        {
            throw ArgumentException.BackupObjectIsAlreadyAdded(backupObject);
        }

        var fullPath = UPath.Combine(Repository.BaseDirectory, backupObject.RelativePath);
        if (!Repository.RepositoryFileSystem.DirectoryExists(fullPath) &&
            !Repository.RepositoryFileSystem.FileExists(fullPath))
        {
            throw PathException.DoesNotExist(fullPath.FullName);
        }

        if (Repository.RepositoryFileSystem.DirectoryExists(fullPath) &&
            Repository.RepositoryFileSystem.FileExists(fullPath))
        {
            throw PathException.DirectoryAndFileWithSamePath(fullPath.FullName);
        }

        _backupObjects.Add(backupObject);
    }

    public void DeleteBackupObject(BackupObject backupObject)
    {
        if (!_backupObjects.Contains(backupObject))
        {
            throw ArgumentException.BackupObjectIsNotAdded();
        }

        _backupObjects.Remove(backupObject);
    }

    public void CreateRestorePoint()
    {
        int id = _restorePoints.Count == 0 ? 0 : _restorePoints.Last().Id + 1;
        var storages = StorageAlgorithm.MakeStorages(id, _backupObjects).ToList();

        var restorePoint = new RestorePoint(id, storages);
        Repository.SaveRestorePoint(restorePoint);
        _restorePoints.Add(restorePoint);
    }
}