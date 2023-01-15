using Backups.Archivers;
using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Models;
using Backups.StorageAlgorithms;
using Backups.Tools.Creators;

namespace Backups.Extra.Entities;

public class BackupTaskExtended : IBackupTask
{
    private readonly IRestorePointDeleteSelector _deleteSelector;
    private readonly IBackupTaskLogger _logger;
    private readonly List<IRestorePoint> _restorePoints = new ();
    private readonly List<IBackupObject> _backupObjects = new ();
    private readonly IStorageAlgorithm _storageAlgorithm;
    private readonly IStorageArchiver _archiver;
    private readonly IRestorePointCreator _restorePointCreator;
    private int _newRestorePointId;

    public BackupTaskExtended(
        IRestorePointDeleteSelector deleteSelector,
        IBackupTaskLogger logger,
        IStorageAlgorithm storageAlgorithm,
        IStorageArchiver archiver,
        IRestorePointCreator restorePointCreator)
    {
        _deleteSelector = deleteSelector;
        _logger = logger;
        _storageAlgorithm = storageAlgorithm;
        _archiver = archiver;
        _restorePointCreator = restorePointCreator;
    }
}