namespace Backups.Repositories;

public interface IRepository
{
    public string RestorePointsPath { get; }
    public string ValidateRelativePath(string path);
    public void CreateDirectory(string path);
    public void CreateFile(string path);
    public void DeleteDirectory(string path);
    public void DeleteFile(string path);
    public bool DirectoryExists(string path);
    public bool FileExists(string path);
    public IReadOnlyCollection<IRepositoryObject> GetRootDirectoryEntries();
    public IRepositoryObject GetRepositoryObject(string path);
    public Stream OpenWrite(string path);
}