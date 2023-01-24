using Backups.Archivers;
using Backups.Extra.Entities;
using Backups.Extra.Loggers;
using Backups.Extra.Models;
using Backups.Models;
using Backups.Repositories;
using Backups.StorageAlgorithms;
using Backups.Tools.Creators;
using Xunit;

namespace Backups.Extra.Test.RestoreTests;

public class RestoreToRepositoryTest
{
    [Fact]
    public void RestorePointToRepository_DataRestored()
    {
        const string restorePointsPath = "RestorePoints";
        var repository = new InMemoryRepository(restorePointsPath);
        repository.CreateFile("file1.txt");
        repository.CreateDirectory("dir1");
        var deleteSelector = new AmountRestorePointDeleteSelector(5);
        var cleaner = new DeleteOldPointsCleaner();
        var logger = new ConsoleLogger(new CurrentTimePrefixGenerator());
        var backupTask = new BackupTaskExtended(
            "TestTask",
            repository,
            deleteSelector,
            cleaner,
            logger,
            new SplitStorageAlgorithm(),
            new ZipStorageArchiver(),
            new RestorePointCreator());
        var fileBackupObject = new BackupObject("file1.txt");
        var dirBackupObject = new BackupObject("dir1");
        backupTask.AddBackupObject(fileBackupObject);
        backupTask.AddBackupObject(dirBackupObject);
        backupTask.CreateRestorePoint();

        backupTask.RemoveBackupObject(fileBackupObject);
        backupTask.RemoveBackupObject(dirBackupObject);
        backupTask.Repository.DeleteFile("file1.txt");
        backupTask.Repository.DeleteDirectory("dir1");

        Assert.False(backupTask.Repository.FileExists("file1.txt"));
        Assert.False(backupTask.Repository.DirectoryExists("dir1"));

        backupTask.RestoreToRepository(backupTask.RestorePoints.Last(), repository);
        Assert.True(backupTask.Repository.FileExists("file1.txt"));
        Assert.True(backupTask.Repository.DirectoryExists("dir1"));
    }
}