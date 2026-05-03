using ECommerce.Domain.Enums;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public string UserId { get; private set; } = string.Empty;
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public List<OrderItem> Items { get; private set; } = new();
    public decimal Total => Items.Sum(i => i.Total);

    protected Order() { }

    public Order(string userId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Status = OrderStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddItem(Product product, int quantity)
    {
        product.DecrementStock(quantity);
        Items.Add(new OrderItem(Id, product.Id, product.Price, quantity));
    }

    public void Confirm()
    {
        if (Status != OrderStatus.Pending)
            throw new DomainException("Apenas pedidos pendentes podem ser confirmados.");
        Status = OrderStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Delivered)
            throw new DomainException("Pedidos entregues não podem ser cancelados.");
        Status = OrderStatus.Cancelled;
    }
}