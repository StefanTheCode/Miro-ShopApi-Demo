namespace ShopApi.Domain.Entities;

public class Customer
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    // A customer can place many orders
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
