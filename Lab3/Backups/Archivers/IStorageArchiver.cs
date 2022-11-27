using Backups.Models;
using Backups.Repositories;

namespace Backups.Archivers;

public interface IStorageArchiver
{
    public IStorageArchive CreateArchive(string name, IRepository repository, IEnumerable<BackupObject> backupObjects);
}