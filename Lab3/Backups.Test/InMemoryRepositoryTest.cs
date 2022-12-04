using Backups.Archivers;
using Backups.Entities;
using Backups.Models;
using Backups.Repositories;
using Backups.StorageAlgorithms;
using Backups.Tools.Creators;
using Xunit;

namespace Backups.Test;

public class InMemoryRepositoryTest
{
    [Fact]
    public void CreateRestorePoints_SplitStoragesCreated()
    {
        const string restorePointsPath = "RestorePoints";
        var repository = new InMemoryRepository(restorePointsPath);
        repository.CreateFile("file1.txt");
        repository.CreateDirectory(Path.Combine("dir1", "dir2"));
        var backupTask = new BackupTask("TestTask", repository, new SplitStorageAlgorithm(), new ZipStorageArchiver(), new RestorePointCreator());
        var fileBackupObject = new BackupObject("file1.txt");
        var dirBackupObject = new BackupObject("dir1");
        backupTask.AddBackupObject(fileBackupObject);
        backupTask.AddBackupObject(dirBackupObject);
        backupTask.CreateRestorePoint();

        Assert.True(backupTask.Repository.FileExists(Path.Combine(restorePointsPath, "file1.txt(1).zip")));
        Assert.True(backupTask.Repository.FileExists(Path.Combine(restorePointsPath, "dir1(1).zip")));
        Assert.Equal(2, backupTask.Repository.GetFileSystemEntries(restorePointsPath).Count());

        backupTask.RemoveBackupObject(dirBackupObject);
        backupTask.CreateRestorePoint();

        Assert.True(backupTask.Repository.FileExists(Path.Combine(restorePointsPath, "file1.txt(2).zip")));
        Assert.Equal(3, backupTask.Repository.GetFileSystemEntries(restorePointsPath).Count());

        backupTask.Repository.DeleteFile(Path.Combine(restorePointsPath, "file1.txt(1).zip"));
        backupTask.Repository.DeleteFile(Path.Combine(restorePointsPath, "file1.txt(2).zip"));
        backupTask.Repository.DeleteFile(Path.Combine(restorePointsPath, "dir1(1).zip"));
    }

    [Fact]
    public void CreateRestorePoint_SingleStorageCreated()
    {
        const string restorePointsPath = "RestorePoints";
        var repository = new InMemoryRepository(restorePointsPath);
        repository.CreateFile("file1.txt");
        repository.CreateDirectory(Path.Combine("dir1", "dir2"));
        var backupTask = new BackupTask("TestTask", repository, new SingleStorageAlgorithm(), new ZipStorageArchiver(), new RestorePointCreator());
        var fileBackupObject = new BackupObject("file1.txt");
        var dirBackupObject = new BackupObject("dir1");
        backupTask.AddBackupObject(fileBackupObject);
        backupTask.AddBackupObject(dirBackupObject);
        backupTask.CreateRestorePoint();

        Assert.True(backupTask.Repository.FileExists(Path.Combine(restorePointsPath, "RestorePoint_1.zip")));
        Assert.Single(backupTask.Repository.GetFileSystemEntries(restorePointsPath));
    }
}