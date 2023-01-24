using Backups.Tools.Exceptions;
using Zio;
using Zio.FileSystems;

namespace Backups.Repositories;

public class InMemoryRepository : IRepository
{
    private readonly MemoryFileSystem _fileSystem = new ();

    public InMemoryRepository(string restorePointsPath)
    {
        RestorePointsPath = ValidateRelativePath(restorePointsPath);
        CreateDirectory(RestorePointsPath);
    }

    public string RestorePointsPath { get; }

    public Stream OpenWrite(string path)
    {
        return _fileSystem.OpenFile(GetAbsolutePath(path), FileMode.Open, FileAccess.Write);
    }

    public void CreateFile(string path)
    {
        Stream file = _fileSystem.CreateFile(GetAbsolutePath(path));
        file.Dispose();
    }

    public void CreateDirectory(string path)
    {
        _fileSystem.CreateDirectory(GetAbsolutePath(path));
    }

    public void DeleteFile(string path)
    {
        _fileSystem.DeleteFile(GetAbsolutePath(path));
    }

    public void DeleteDirectory(string path)
    {
        _fileSystem.DeleteDirectory(GetAbsolutePath(path), true);
    }

    public bool FileExists(string path)
    {
        return _fileSystem.FileExists(GetAbsolutePath(path));
    }

    public bool DirectoryExists(string path)
    {
        return _fileSystem.DirectoryExists(GetAbsolutePath(path));
    }

    public IEnumerable<string> GetFileSystemEntries(string path)
    {
        IEnumerable<UPath> paths = _fileSystem.EnumeratePaths(GetAbsolutePath(path));
        return paths.Select(p => Path.GetFileName(p.FullName));
    }

    public IRepositoryObject GetRepositoryObject(string path)
    {
        ValidateRelativePath(path);
        if (FileExists(path))
        {
            return new InMemoryRepositoryFile(path, _fileSystem);
        }

        if (!DirectoryExists(path))
        {
            throw RepositoryException.DirectoryNotFound(path);
        }

        var entries = new List<IRepositoryObject>();
        foreach (string entry in GetFileSystemEntries(path))
        {
            entries.Add(GetRepositoryObject(Path.Combine(path, entry)));
        }

        return new InMemoryRepositoryFolder(path, entries);
    }

    public IReadOnlyCollection<IRepositoryObject> GetRootDirectoryEntries()
    {
        return _fileSystem.EnumeratePaths("/").Select(entry => GetRepositoryObject(entry.FullName)).ToList();
    }

    public string ValidateRelativePath(string path)
    {
        if (path == string.Empty)
        {
            throw BackupsArgumentException.EmptyPathString();
        }

        if (Path.IsPathRooted(path) || Path.EndsInDirectorySeparator(path) || path.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
        {
            throw RepositoryException.IncorrectRelativePath(path);
        }

        return path;
    }

    private string GetAbsolutePath(string path)
    {
        return Path.Combine(Path.DirectorySeparatorChar.ToString(), ValidateRelativePath(path));
    }
}