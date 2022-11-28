using Backups.Models;
using Backups.Repositories;

namespace Backups.Archivers;

public interface IStorageArchiver
{
    public string ArchiveExtension { get; }
    public IStorageArchive CreateArchive(string name, IRepository repository, IBackupObject backupObject);
    public IStorageArchive CreateArchive(string name, IRepository repository, IEnumerable<IBackupObject> backupObjects);
}