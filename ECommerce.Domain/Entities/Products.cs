using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category? Category { get; private set; }
    public DateTime CreatedAt { get; private set; }

    protected Product() { }

    public Product(string name, string description, decimal price, int stock, Guid categoryId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        CategoryId = categoryId;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
    }

    public void UpdateStock(int quantity) => Stock = quantity;

    public void DecrementStock(int quantity)
    {
        if (quantity > Stock)
            throw new DomainException("Estoque insuficiente.");
        Stock -= quantity;
    }
}