namespace ShopApi.Domain.Entities;

public class DiscountCode
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public decimal Percentage { get; set; }        // e.g. 10 = 10% off
    public DateTime ExpiresAtUtc { get; set; }
}
