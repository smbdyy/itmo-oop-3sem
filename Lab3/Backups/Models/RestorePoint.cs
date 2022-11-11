namespace Backups.Models;

public class RestorePoint
{
    private List<Storage> _storages;

    public RestorePoint(int id, string name, IEnumerable<Storage> storages)
    {
        if (name == string.Empty)
        {
            throw new NotImplementedException();
        }

        _storages = new List<Storage>(storages);
        Id = id;
        CreationDateTime = DateTime.Now;
        Name = name;
    }

    public IReadOnlyList<Storage> Storages => _storages;
    public int Id { get; }
    public DateTime CreationDateTime { get; }
    public string Name { get; }
}