using Backups.Archivers;
using Backups.Models;
using Backups.Repositories;
using Backups.RestorePoints.Creators;
using Backups.Services;
using Backups.StorageAlgorithms;
using Xunit;

namespace Backups.Test;

public class LocalFileSystemTest
{
    [Fact(Skip = "local fs")]
    public void CreateRestorePoints_SplitStoragesCreated()
    {
        const string restorePointsPath = "RestorePoints";
        var repository = new FileSystemRepository(@"C:\BackupsLab", restorePointsPath);
        repository.CreateFile("file1.txt");
        repository.CreateDirectory(Path.Combine("dir1", "dir2"));
        var backupTask = new BackupTask("TestTask", repository, new SplitStorageAlgorithm(), new ZipStorageArchiver(), new RestorePointCreator());
        var fileBackupObject = new BackupObject("file1.txt");
        var dirBackupObject = new BackupObject("dir1");
        backupTask.AddBackupObject(fileBackupObject);
        backupTask.AddBackupObject(dirBackupObject);
        backupTask.CreateRestorePoint();

        string restorePointFolderPath = Path.Combine(restorePointsPath, backupTask.RestorePoints.Last().FolderName);
        var restorePointFolder = (IRepositoryFolder)repository.GetRepositoryObject(restorePointFolderPath);
        Assert.True(backupTask.Repository.FileExists(Path.Combine(restorePointFolderPath, "file1.txt(1).zip")));
        Assert.True(backupTask.Repository.FileExists(Path.Combine(restorePointFolderPath, "dir1(1).zip")));
        Assert.Equal(2, restorePointFolder.Entries.Count);

        backupTask.RemoveBackupObject(dirBackupObject);
        backupTask.CreateRestorePoint();

        restorePointFolderPath = Path.Combine(restorePointsPath, backupTask.RestorePoints.Last().FolderName);
        restorePointFolder = (IRepositoryFolder)repository.GetRepositoryObject(restorePointFolderPath);
        Assert.True(backupTask.Repository.FileExists(Path.Combine(restorePointFolderPath, "file1.txt(2).zip")));
        Assert.Equal(1, restorePointFolder.Entries.Count);

        backupTask.Repository.DeleteFile(Path.Combine(restorePointFolderPath, "file1.txt(1).zip"));
        backupTask.Repository.DeleteFile(Path.Combine(restorePointFolderPath, "file1.txt(2).zip"));
        backupTask.Repository.DeleteFile(Path.Combine(restorePointFolderPath, "dir1(1).zip"));
    }

    [Fact(Skip = "local fs")]
    public void CreateRestorePoint_SingleStorageCreated()
    {
        const string restorePointsPath = "RestorePoints";
        var repository = new FileSystemRepository(@"C:\BackupsLab", restorePointsPath);
        repository.CreateFile("file1.txt");
        repository.CreateDirectory(Path.Combine("dir1", "dir2"));
        var backupTask = new BackupTask("TestTask", repository, new SingleStorageAlgorithm(), new ZipStorageArchiver(), new RestorePointCreator());
        var fileBackupObject = new BackupObject("file1.txt");
        var dirBackupObject = new BackupObject("dir1");
        backupTask.AddBackupObject(fileBackupObject);
        backupTask.AddBackupObject(dirBackupObject);
        backupTask.CreateRestorePoint();

        string restorePointFolderPath = Path.Combine(restorePointsPath, backupTask.RestorePoints.Last().FolderName);
        var restorePointFolder = (IRepositoryFolder)repository.GetRepositoryObject(restorePointFolderPath);
        Assert.True(backupTask.Repository.FileExists(Path.Combine(restorePointFolderPath, "RestorePoint_1.zip")));
        Assert.Single(restorePointFolder.Entries);
    }
}