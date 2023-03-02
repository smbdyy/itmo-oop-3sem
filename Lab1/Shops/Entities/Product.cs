namespace Shops.Entities;

public class Product
{
    public Product(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Name { get; }
    public Guid Id { get; }
}