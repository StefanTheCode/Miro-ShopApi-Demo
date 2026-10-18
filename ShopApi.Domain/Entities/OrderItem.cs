namespace ShopApi.Domain.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    // FK to Order (one order -> many items)
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    // FK to Product (one product -> many items)
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}
