using Backups.Archivers;
using Backups.Repositories;

namespace Backups.StorageAlgorithms;

public class SplitStorageAlgorithm : IStorageAlgorithm
{
    public IStorage MakeStorage(
        int id, IRepository repository, IStorageArchiver storageArchiver, IEnumerable<IRepositoryObject> objects)
    {
        var archiveEntries = new List<IRepositoryObject>();

        foreach (IRepositoryObject entry in objects)
        {
            string archiveName = $"{Path.GetFileName(entry.Path)}({id}){storageArchiver.ArchiveExtension}";
            archiveEntries.AddRange(storageArchiver.CreateArchive(
                archiveName, repository, new List<IRepositoryObject> { entry }));
        }

        return new Storage(archiveEntries);
    }
}