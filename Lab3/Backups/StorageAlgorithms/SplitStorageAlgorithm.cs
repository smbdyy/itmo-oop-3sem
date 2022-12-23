using Backups.Archivers;
using Backups.Models;
using Backups.Repositories;

namespace Backups.StorageAlgorithms;

public class SplitStorageAlgorithm : IStorageAlgorithm
{
    public IStorage MakeStorage(int id, IRepository repository, IStorageArchiver storageArchiver, IEnumerable<IRepositoryObject> objects)
    {
        var archives = new List<IStorageArchive>();
        foreach (IRepositoryObject repositoryObject in objects)
        {
            string archiveName = $"{Path.GetFileName(repositoryObject.Path)}({id})";
            archives.Add(storageArchiver.CreateArchive(archiveName, repository, repositoryObject));
        }

        return new SplitStorage(archives);
    }
}