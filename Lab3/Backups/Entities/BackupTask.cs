using Backups.Models;
using Backups.Repositories;
using Backups.StorageAlgorithms;

namespace Backups.Entities;

public class BackupTask
{
    private readonly List<RestorePoint> _restorePoints = new ();
    private readonly List<BackupObject> _backupObjects = new ();

    public BackupTask(string name, IRepository repository, IStorageAlgorithm storageAlgorithm)
    {
        Name = ValidateName(name);
        Repository = repository;
        StorageAlgorithm = storageAlgorithm;
    }

    public string Name { get; }
    public IRepository Repository { get; }
    public IStorageAlgorithm StorageAlgorithm { get; }
    public IReadOnlyCollection<RestorePoint> RestorePoints => _restorePoints;
    public IReadOnlyCollection<BackupObject> BackupObjects => _backupObjects;

    private static string ValidateName(string value)
    {
        if (value == string.Empty)
        {
            throw new NotImplementedException();
        }

        if (value.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
        {
            throw new NotImplementedException();
        }

        return value;
    }
}