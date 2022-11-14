﻿using Backups.Models;

namespace Backups.StorageAlgorithms;

public class SplitStorageAlgorithm : IStorageAlgorithm
{
    public IEnumerable<Storage> MakeStorages(int id, IEnumerable<BackupObject> backupObjects)
    {
        IEnumerable<BackupObject> backupObjectsList = backupObjects.ToList();
        if (!backupObjectsList.Any())
        {
            throw new NotImplementedException();
        }

        var storages = new List<Storage>();
        foreach (BackupObject backupObject in backupObjectsList)
        {
            string fileName = Path.GetFileName(backupObject.RelativePath.FullName);
            string storageName = $"{fileName}({id}).zip";
            var storage = new Storage(storageName, new List<BackupObject> { backupObject });
            storages.Add(storage);
        }

        return storages;
    }
}