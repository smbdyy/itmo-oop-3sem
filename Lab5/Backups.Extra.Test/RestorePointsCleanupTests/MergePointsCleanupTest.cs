using Backups.Archivers;
using Backups.Extra.Entities;
using Backups.Extra.Models;
using Backups.Models;
using Backups.Repositories;
using Backups.StorageAlgorithms;
using Backups.Tools.Creators;
using Xunit;

namespace Backups.Extra.Test.RestorePointsCleanupTests;

public class MergePointsCleanupTest
{
    [Fact]
    public void MergeOldRestorePointToNew_OldFileCopied()
    {
        const string restorePointsPath = "RestorePoints";
        var repository = new InMemoryRepository(restorePointsPath);
        repository.CreateFile("file1.txt");
        repository.CreateFile("file2.txt");
        var deleteSelector = new AmountRestorePointDeleteSelector(2);
        var cleaner = new MergePointsCleaner();
        var logger = new BackupTaskLoggerStub();
        var backupTask = new BackupTaskExtended(
            "TestTask",
            repository,
            deleteSelector,
            cleaner,
            logger,
            new SplitStorageAlgorithm(),
            new ZipStorageArchiver(),
            new RestorePointCreator());

        var backupObject1 = new BackupObject("file1.txt");
        var backupObject2 = new BackupObject("file2.txt");
        backupTask.AddBackupObject(backupObject1);
        backupTask.AddBackupObject(backupObject2);
        backupTask.CreateRestorePoint();
        backupTask.RemoveBackupObject(backupObject2);
        backupTask.CreateRestorePoint();

        IRestorePoint lastPoint = backupTask.RestorePoints.Last();
        Assert.DoesNotContain(lastPoint.Storage.GetEntries(), entry => entry.Path == "file2.txt");

        backupTask.CreateRestorePoint();
        lastPoint = backupTask.RestorePoints.Last();
        Assert.Contains(lastPoint.Storage.GetEntries(), entry => entry.Path == "file2.txt");
    }
}