using MaualDomainEvents.DomainEvents;

namespace MaualDomainEvents.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }

    // event domain producer look like:
    public Product(string name)
    {
        Name = name;

        DomainEventsManager.Raise(new ProductAdded(this));
    }
}