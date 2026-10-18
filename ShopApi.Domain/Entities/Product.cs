namespace ShopApi.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }

    // A product can appear in many order items
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
