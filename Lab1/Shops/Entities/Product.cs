namespace Shops.Entities;

public class Product
{
    public Product(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public string Name { get; }
    public Guid Id { get; }
}