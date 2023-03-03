using Backups.Archivers;
using Backups.Extra.Creators;
using Backups.Extra.Entities;
using Backups.Extra.Interfaces;
using Backups.Extra.Loggers;
using Backups.Extra.Models;
using Backups.Models;
using Backups.Repositories;
using Backups.RestorePoints.Creators;
using Backups.StorageAlgorithms;
using Xunit;

namespace Backups.Extra.Test.RestorePointsCleanupTests;

public class DeleteOldPointsCleanupTest
{
    [Fact]
    public void ExceedAmountLimit_OldRestorePointDeleted()
    {
        const string restorePointsPath = "RestorePoints";
        var repository = new InMemoryRepository(restorePointsPath);
        repository.CreateFile("file1.txt");
        var deleteSelector = new AmountRestorePointDeleteSelector(2);
        var cleaner = new DeleteOldPointsCleaner();
        var logger = new ConsoleLogger(new CurrentTimePrefixGenerator());
        var backupTask = new BackupTaskExtended(
            "TestTask",
            repository,
            deleteSelector,
            cleaner,
            logger,
            new SingleStorageAlgorithm(),
            new ZipStorageArchiver(),
            new RestorePointCreator());

        backupTask.AddBackupObject(new BackupObject("file1.txt"));
        backupTask.CreateRestorePoint();
        backupTask.CreateRestorePoint();
        backupTask.CreateRestorePoint();

        Assert.Equal(2, backupTask.RestorePoints.Count);
        Assert.False(backupTask.Repository.DirectoryExists(Path.Combine(restorePointsPath, "RestorePoint_1")));
        Assert.True(backupTask.Repository.DirectoryExists(Path.Combine(restorePointsPath, "RestorePoint_2")));
        Assert.True(backupTask.Repository.DirectoryExists(Path.Combine(restorePointsPath, "RestorePoint_3")));
    }

    [Fact]
    public void ExpireDateLimit_OldRestorePointDeleted()
    {
        const string restorePointsPath = "RestorePoints";
        var repository = new InMemoryRepository(restorePointsPath);
        repository.CreateFile("file1.txt");
        var deleteSelector = new DateRestorePointDeleteCreator(new DateTime(2023, 1, 10));
        var cleaner = new DeleteOldPointsCleaner();
        var logger = new ConsoleLogger(new CurrentTimePrefixGenerator());
        var backupTask = new BackupTaskExtended(
            "TestTask",
            repository,
            deleteSelector,
            cleaner,
            logger,
            new SingleStorageAlgorithm(),
            new ZipStorageArchiver(),
            new RestorePointWithDateSetterCreator());

        backupTask.AddBackupObject(new BackupObject("file1.txt"));
        backupTask.CreateRestorePoint();
        var createdPoint = (RestorePointWithDateSetter)backupTask.RestorePoints.Last();
        createdPoint.CreationDateTime = new DateTime(2023, 1, 9);
        backupTask.CreateRestorePoint();

        Assert.Single(backupTask.RestorePoints);
        Assert.False(backupTask.Repository.DirectoryExists(Path.Combine(restorePointsPath, "RestorePoint_1")));
        Assert.True(backupTask.Repository.DirectoryExists(Path.Combine(restorePointsPath, "RestorePoint_2")));
    }

    [Fact]
    public void ExceedOneLimitInAnyLimitDeleteSelector_OldRestorePointDeleted()
    {
        const string restorePointsPath = "RestorePoints";
        var repository = new InMemoryRepository(restorePointsPath);
        repository.CreateFile("file1.txt");
        var deleteSelectors = new List<IRestorePointDeleteSelector>
        {
            new AmountRestorePointDeleteSelector(2),
            new DateRestorePointDeleteCreator(new DateTime(2023, 1, 10)),
        };
        var cleaner = new DeleteOldPointsCleaner();
        var logger = new ConsoleLogger(new CurrentTimePrefixGenerator());
        var backupTask = new BackupTaskExtended(
            "TestTask",
            repository,
            new AnyLimitRestorePointDeleteSelector(deleteSelectors),
            cleaner,
            logger,
            new SingleStorageAlgorithm(),
            new ZipStorageArchiver(),
            new RestorePointCreator());

        backupTask.AddBackupObject(new BackupObject("file1.txt"));
        backupTask.CreateRestorePoint();
        backupTask.CreateRestorePoint();
        backupTask.CreateRestorePoint();

        Assert.Equal(2, backupTask.RestorePoints.Count);
        Assert.False(backupTask.Repository.DirectoryExists(Path.Combine(restorePointsPath, "RestorePoint_1")));
        Assert.True(backupTask.Repository.DirectoryExists(Path.Combine(restorePointsPath, "RestorePoint_2")));
        Assert.True(backupTask.Repository.DirectoryExists(Path.Combine(restorePointsPath, "RestorePoint_3")));
    }

    [Fact]
    public void ExceedOnlyOneLimitInEveryLimitSelector_AllPointsRemained()
    {
        const string restorePointsPath = "RestorePoints";
        var repository = new InMemoryRepository(restorePointsPath);
        repository.CreateFile("file1.txt");
        var deleteSelectors = new List<IRestorePointDeleteSelector>
        {
            new AmountRestorePointDeleteSelector(2),
            new DateRestorePointDeleteCreator(new DateTime(2023, 1, 10)),
        };
        var cleaner = new DeleteOldPointsCleaner();
        var logger = new ConsoleLogger(new CurrentTimePrefixGenerator());
        var backupTask = new BackupTaskExtended(
            "TestTask",
            repository,
            new EveryLimitRestorePointDeleteSelector(deleteSelectors),
            cleaner,
            logger,
            new SingleStorageAlgorithm(),
            new ZipStorageArchiver(),
            new RestorePointCreator());

        backupTask.AddBackupObject(new BackupObject("file1.txt"));
        backupTask.CreateRestorePoint();
        backupTask.CreateRestorePoint();
        backupTask.CreateRestorePoint();

        Assert.Equal(3, backupTask.RestorePoints.Count);
        Assert.True(backupTask.Repository.DirectoryExists(Path.Combine(restorePointsPath, "RestorePoint_1")));
        Assert.True(backupTask.Repository.DirectoryExists(Path.Combine(restorePointsPath, "RestorePoint_2")));
        Assert.True(backupTask.Repository.DirectoryExists(Path.Combine(restorePointsPath, "RestorePoint_3")));
    }

    [Fact]
    public void ExceedEveryLimitInEveryLimitSelector_OldPointDeleted()
    {
        const string restorePointsPath = "RestorePoints";
        var repository = new InMemoryRepository(restorePointsPath);
        repository.CreateFile("file1.txt");
        var deleteSelectors = new List<IRestorePointDeleteSelector>
        {
            new AmountRestorePointDeleteSelector(2),
            new DateRestorePointDeleteCreator(new DateTime(2023, 1, 10)),
        };
        var cleaner = new DeleteOldPointsCleaner();
        var logger = new ConsoleLogger(new CurrentTimePrefixGenerator());
        var backupTask = new BackupTaskExtended(
            "TestTask",
            repository,
            new EveryLimitRestorePointDeleteSelector(deleteSelectors),
            cleaner,
            logger,
            new SingleStorageAlgorithm(),
            new ZipStorageArchiver(),
            new RestorePointWithDateSetterCreator());

        backupTask.AddBackupObject(new BackupObject("file1.txt"));
        backupTask.CreateRestorePoint();
        backupTask.CreateRestorePoint();
        var firstPoint = (RestorePointWithDateSetter)backupTask.RestorePoints.First();
        firstPoint.CreationDateTime = new DateTime(2023, 1, 9);
        backupTask.CreateRestorePoint();

        Assert.Equal(2, backupTask.RestorePoints.Count);
        Assert.False(backupTask.Repository.DirectoryExists(Path.Combine(restorePointsPath, "RestorePoint_1")));
        Assert.True(backupTask.Repository.DirectoryExists(Path.Combine(restorePointsPath, "RestorePoint_2")));
        Assert.True(backupTask.Repository.DirectoryExists(Path.Combine(restorePointsPath, "RestorePoint_3")));
    }
}