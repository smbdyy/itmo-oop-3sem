﻿using Backups.StorageAlgorithms;

namespace Backups.Models;

public interface IRestorePoint
{
    public string FolderName { get; }
    public IReadOnlyCollection<IBackupObject> BackupObjects { get; }
    public DateTime CreationDateTime { get; }
    public IStorage Storage { get; }
}