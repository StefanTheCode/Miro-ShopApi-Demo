namespace ShopApi.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    // FK to Customer (one customer -> many orders)
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    // One order -> many order items
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

    // Applied discount (0 = none)
    public decimal DiscountPercentage { get; set; }

    public decimal Total =>
        Items.Sum(i => i.UnitPrice * i.Quantity) * (1 - DiscountPercentage / 100m);
}
