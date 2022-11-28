using Backups.Tools;

namespace Backups.Repositories;

public class FileSystemRepository : IRepository
{
    public FileSystemRepository(string rootPath, string restorePointsPath)
    {
        RootPath = FormatRootPath(rootPath);
        RestorePointsPath = ValidateRelativePath(restorePointsPath);
        CreateDirectory(RestorePointsPath);
    }

    public string RootPath { get; }
    public string RestorePointsPath { get; }

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

    public IEnumerable<string> EnumerateDirectories(string path)
    {
        ValidateRelativePath(path);
        return Directory.EnumerateDirectories(Path.Combine(RootPath, path));
    }

    public IEnumerable<string> EnumerateFiles(string path)
    {
        ValidateRelativePath(path);
        return Directory.EnumerateFiles(Path.Combine(RootPath, path));
    }

    public IEnumerable<string> EnumerateFileSystemEntries(string path)
    {
        ValidateRelativePath(path);
        return Directory.EnumerateFileSystemEntries(Path.Combine(RootPath, path));
    }

    public Stream OpenWrite(string path)
    {
        ValidateRelativePath(path);
        if (!FileExists(path))
        {
            throw RepositoryException.FileNotFound(path);
        }

        return File.OpenWrite(Path.Combine(RootPath, path));
    }

    public Stream OpenRead(string path)
    {
        ValidateRelativePath(path);
        if (!FileExists(path))
        {
            throw RepositoryException.FileNotFound(path);
        }

        return File.OpenRead(Path.Combine(RootPath, path));
    }

    public IReadOnlyCollection<IRepositoryObject> GetRootDirectoryEntries()
    {
        return Directory.EnumerateFileSystemEntries(RootPath).Select(GetRepositoryObject).ToList();
    }

    public IRepositoryObject GetRepositoryObject(string path)
    {
        ValidateRelativePath(path);
        if (FileExists(path))
        {
            return new FileSystemRepositoryFile(path, this);
        }

        if (!DirectoryExists(path))
        {
            throw RepositoryException.DirectoryNotFound(path);
        }

        var entries = new List<IRepositoryObject>();
        foreach (string entry in EnumerateDirectories(path))
        {
            entries.Add(GetRepositoryObject(Path.Combine(path, entry)));
        }

        return new FileSystemRepositoryFolder(path, entries);
    }

    public string ValidateRelativePath(string path)
    {
        if (path == string.Empty)
        {
            throw BackupsArgumentException.EmptyPathString();
        }

        if (Path.IsPathRooted(path) || Path.EndsInDirectorySeparator(path) || path.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
        {
            throw RepositoryException.IncorrectRootPath(path);
        }

        return path;
    }

    private static string FormatRootPath(string path)
    {
        string rootPath = Path.GetFullPath(path);
        if (!Directory.Exists(rootPath))
        {
            throw RepositoryException.DirectoryNotFound(path);
        }

        if (!Path.EndsInDirectorySeparator(rootPath))
        {
            rootPath += Path.DirectorySeparatorChar;
        }

        return rootPath;
    }
}