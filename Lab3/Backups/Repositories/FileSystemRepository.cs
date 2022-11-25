using System.Security.Policy;

namespace Backups.Repositories;

public class FileSystemRepository : IRepository
{
    public FileSystemRepository(string rootPath)
    {
        RootPath = Path.GetFullPath(rootPath);
        if (!Directory.Exists(rootPath))
        {
            throw new NotImplementedException();
        }

        if (!Path.EndsInDirectorySeparator(rootPath))
        {
            rootPath += Path.DirectorySeparatorChar;
        }
    }

    public string RootPath { get; }

    public void CreateDirectory(string path)
    {
        ValidateRelativePath(path);
        Directory.CreateDirectory(Path.Combine(RootPath, path));
    }

    public void CreateFile(string path)
    {
        ValidateRelativePath(path);
        File.Create(Path.Combine(RootPath, path));
    }

    public void DeleteDirectory(string path)
    {
        ValidateRelativePath(path);
        Directory.Delete(Path.Combine(RootPath, path));
    }

    public void DeleteFile(string path)
    {
        ValidateRelativePath(path);
        File.Delete(Path.Combine(RootPath, path));
    }

    public bool DirectoryExists(string path)
    {
        ValidateRelativePath(path);
        return Directory.Exists(Path.Combine(RootPath, path));
    }

    public bool FileExists(string path)
    {
        ValidateRelativePath(path);
        return File.Exists(Path.Combine(RootPath, path));
    }

    public IReadOnlyCollection<IRepositoryObject> GetRootDirectoryEntries()
    {
        var entries = new List<IRepositoryObject>();
        foreach (string file in Directory.EnumerateFiles(RootPath))
        {
            entries.Add(new FileSystemRepositoryFile(Path.Combine(RootPath, file)));
        }

        foreach (string folder in Directory.EnumerateDirectories(RootPath))
        {
            entries.Add(GetRepositoryObject(folder));
        }

        return entries;
    }

    public IRepositoryObject GetRepositoryObject(string path)
    {
        string fullPath = Path.Combine(RootPath, path);
        if (File.Exists(fullPath))
        {
            return new FileSystemRepositoryFile(fullPath);
        }

        var entries = new List<IRepositoryObject>();
        foreach (string file in Directory.EnumerateFiles(fullPath))
        {
            entries.Add(new FileSystemRepositoryFile(Path.Combine(RootPath, file)));
        }

        foreach (string folder in Directory.EnumerateDirectories(fullPath))
        {
            entries.Add(GetRepositoryObject(Path.Combine(path, folder)));
        }

        return new FileSystemRepositoryFolder(entries);
    }

    private static void ValidateRelativePath(string path)
    {
        if (Path.IsPathRooted(path) || Path.EndsInDirectorySeparator(path) || path.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
        {
            throw new NotImplementedException();
        }
    }
}