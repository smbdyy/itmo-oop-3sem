using Backups.Archivers;
using Backups.Repositories;

namespace Backups.StorageAlgorithms;

public class SplitStorageAlgorithm : IStorageAlgorithm
{
    public IStorage MakeStorage(
        int id, string path, IRepository repository, IStorageArchiver storageArchiver, IEnumerable<IRepositoryObject> objects)
    {
        var archiveEntries = new List<IRepositoryObject>();

        foreach (IRepositoryObject entry in objects)
        {
            string archiveName = $"{Path.GetFileName(entry.Path)}({id}){storageArchiver.ArchiveExtension}";
            string archivePath = Path.Combine(path, archiveName);
            repository.CreateFile(archivePath);
            archiveEntries.AddRange(storageArchiver.CreateArchive(
                archivePath, repository, new List<IRepositoryObject> { entry }));
        }

        return new Storage(archiveEntries);
    }
}