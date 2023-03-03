using Backups.Archivers;
using Backups.Repositories;

namespace Backups.StorageAlgorithms;

public class SingleStorageAlgorithm : IStorageAlgorithm
{
    public IStorage MakeStorage(
        int id, string path, IRepository repository, IStorageArchiver storageArchiver, IEnumerable<IRepositoryObject> objects)
    {
        string archivePath = Path.Combine(path, $"RestorePoint_{id}{storageArchiver.ArchiveExtension}");
        repository.CreateFile(archivePath);
        return new Storage(storageArchiver.CreateArchive(archivePath, repository, objects));
    }
}