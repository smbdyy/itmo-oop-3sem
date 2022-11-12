namespace Backups.Models;

public class RestorePoint
{
    private List<Storage> _storages;

    public RestorePoint(int id, IEnumerable<Storage> storages)
    {
        _storages = storages.ToList();

        if (_storages.Count == 0)
        {
            throw new NotImplementedException();
        }

        Id = id;
        CreationDateTime = DateTime.Now;
    }

    public IReadOnlyList<Storage> Storages => _storages;
    public int Id { get; }
    public DateTime CreationDateTime { get; }
}