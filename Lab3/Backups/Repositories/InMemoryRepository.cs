using Backups.Tools.Exceptions;
using Backups.Tools.Utils;
using Backups.Visitors;

namespace Backups.Repositories;

public class InMemoryRepository : IRepository
{
    private const int DefaultBufferSizeDefaultValue = 100;
    private readonly List<IMemoryFileSystemObject> _rootDirectoryEntries = new ();

    public InMemoryRepository(string restorePointsPath, int defaultBufferSize = DefaultBufferSizeDefaultValue)
    {
        if (defaultBufferSize <= 0)
        {
            throw new BackupsArgumentException("default file buffer size must be a positive number");
        }

        DefaultBufferSize = 100;
        RestorePointsPath = ValidateRelativePath(restorePointsPath);
    }

    public string RestorePointsPath { get; }
    public int DefaultBufferSize { get; }

    public Stream OpenWrite(string path)
    {
        ValidateRelativePath(path);
        IMemoryFileSystemFile? file = FindFile(path);
        if (file is null)
        {
            throw new NotImplementedException();
        }

        return file.OpenWrite();
    }

    public IReadOnlyCollection<IRepositoryObject> GetRootDirectoryEntries()
    {
        var entries = new List<IRepositoryObject>();
        foreach (IMemoryFileSystemObject entry in _rootDirectoryEntries)
        {
            var visitor = new InMemoryRepositoryFileSystemVisitor();
            entry.Accept(visitor);
            entries.Add(visitor.GetComposite());
        }

        return entries;
    }

    public void CreateFile(string path)
    {
        CreateFile(path, DefaultBufferSize);
    }

    public void CreateFile(string path, int bufferSize)
    {
        ValidateRelativePath(path);

        string directory = Path.GetDirectoryName(path) ?? string.Empty;

        if (directory == string.Empty)
        {
            if (_rootDirectoryEntries.All(entry => entry.Path != path))
            {
                _rootDirectoryEntries.Add(new MemoryFileSystemFile(path, bufferSize));
            }

            return;
        }

        CreateDirectory(directory);
        IMemoryFileSystemFolder? folder = FindFolder(directory);
        if (folder is null)
        {
            throw new NotImplementedException();
        }

        if (folder.Entries.All(entry => entry.Path != path))
        {
            folder.AddEntry(new MemoryFileSystemFile(path, bufferSize));
        }
    }

    public void CreateDirectory(string path)
    {
        ValidateRelativePath(path);
        string[] splitPath = MemoryFileSystemUtils.SplitPath(path).ToArray();
        string currentPath = splitPath[0];

        IMemoryFileSystemFolder? currentFolder = FindFolder(splitPath[0]);
        if (currentFolder is null)
        {
            currentFolder = new MemoryFileSystemFolder(splitPath[0]);
            _rootDirectoryEntries.Add(currentFolder);
        }

        for (int i = 1; i < splitPath.Length; i++)
        {
            currentPath = Path.Combine(currentPath, splitPath[i]);
            IMemoryFileSystemFolder newFolder = FindFolder(currentPath) ?? new MemoryFileSystemFolder(currentPath);
            currentFolder.AddEntry(newFolder);
            currentFolder = newFolder;
        }
    }

    public void DeleteFile(string path)
    {
        ValidateRelativePath(path);
        string directory = Path.GetDirectoryName(path) ?? string.Empty;
        IMemoryFileSystemFile? file = FindFile(path);
        if (file is null)
        {
            return;
        }

        if (directory == string.Empty)
        {
            _rootDirectoryEntries.Remove(file);
        }
        else
        {
            IMemoryFileSystemFolder? folder = FindFolder(path);
            folder?.DeleteEntry(file);
        }
    }

    public void DeleteDirectory(string path)
    {
        ValidateRelativePath(path);
        string parentDirectory = Path.GetDirectoryName(path) ?? string.Empty;
        IMemoryFileSystemFolder? folder = FindFolder(path);

        if (folder is null)
        {
            return;
        }

        if (parentDirectory == string.Empty)
        {
            _rootDirectoryEntries.Remove(folder);
        }
    }

    public bool FileExists(string path)
    {
        ValidateRelativePath(path);
        return FindFile(path) is not null;
    }

    public bool DirectoryExists(string path)
    {
        ValidateRelativePath(path);
        return FindFolder(path) is not null;
    }

    public IEnumerable<string> GetFiles(string path)
    {
        IMemoryFileSystemFolder? folder = FindFolder(path);
        if (folder is null)
        {
            throw new NotImplementedException();
        }

        var fileNames = new List<string>();
        foreach (IMemoryFileSystemObject entry in folder.Entries)
        {
            if (entry.IsFile)
            {
                fileNames.Add(Path.GetFileName(entry.Path));
            }
        }

        return fileNames;
    }

    public IEnumerable<string> GetDirectories(string path)
    {
        IMemoryFileSystemFolder? folder = FindFolder(path);
        if (folder is null)
        {
            throw new NotImplementedException();
        }

        var dirNames = new List<string>();
        foreach (IMemoryFileSystemObject entry in folder.Entries)
        {
            if (!entry.IsFile)
            {
                dirNames.Add(Path.GetFileName(entry.Path));
            }
        }

        return dirNames;
    }

    public IEnumerable<string> GetFileSystemEntries(string path)
    {
        IMemoryFileSystemFolder? folder = FindFolder(path);
        if (folder is null)
        {
            throw new NotImplementedException();
        }

        return folder.Entries.Select(entry => Path.GetFileName(entry.Path));
    }

    public IRepositoryObject GetRepositoryObject(string path)
    {
        var visitor = new InMemoryRepositoryFileSystemVisitor();
        IMemoryFileSystemObject? found = FindFile(path);
        if (found is not null)
        {
            found.Accept(visitor);
            return visitor.GetComposite();
        }

        found = FindFolder(path);
        if (found is not null)
        {
            found.Accept(visitor);
            return visitor.GetComposite();
        }

        throw new NotImplementedException();
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

        if (path.Split(Path.DirectorySeparatorChar).Length != MemoryFileSystemUtils.SplitPath(path).Count())
        {
            throw new RepositoryException("relative path must not contain empty entries");
        }

        return path;
    }

    private IMemoryFileSystemFolder? FindFolder(string path)
    {
        var visitor = new MemoryFolderFinderVisitor(path);
        foreach (IMemoryFileSystemObject entry in _rootDirectoryEntries)
        {
            entry.Accept(visitor);
            IMemoryFileSystemFolder? found = visitor.GetFound();
            if (found is not null)
            {
                return found;
            }
        }

        return null;
    }

    private IMemoryFileSystemFile? FindFile(string path)
    {
        var visitor = new MemoryFileFinderVisitor(path);
        foreach (IMemoryFileSystemObject entry in _rootDirectoryEntries)
        {
            entry.Accept(visitor);
            IMemoryFileSystemFile? found = visitor.GetFound();
            if (found is not null)
            {
                return found;
            }
        }

        return null;
    }
}