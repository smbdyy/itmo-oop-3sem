using Backups.Tools.Exceptions;

namespace Backups.Repositories;

public class InMemoryRepository : IRepository
{
    private readonly List<IMemoryFileSystemObject> _rootDirectoryEntries = new ();

    public InMemoryRepository(string restorePointsPath)
    {
        RestorePointsPath = ValidateRelativePath(restorePointsPath);
    }

    public string RestorePointsPath { get; }

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
}