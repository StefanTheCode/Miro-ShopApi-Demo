using Microsoft.EntityFrameworkCore;
using ShopApi.Domain.Entities;

namespace ShopApi.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        await db.Database.EnsureCreatedAsync();

        if (await db.Customers.AnyAsync())
            return;

        var customer = new Customer { FullName = "Ada Lovelace", Email = "ada@example.com" };

        var products = new List<Product>
        {
            new() { Name = "Mechanical Keyboard", Price = 120.00m, StockQuantity = 50 },
            new() { Name = "Ultrawide Monitor",   Price = 450.00m, StockQuantity = 20 },
            new() { Name = "USB-C Hub",            Price = 39.90m,  StockQuantity = 100 }
        };

        var discounts = new List<DiscountCode>
        {
            new() { Code = "WELCOME10", Percentage = 10m,  ExpiresAtUtc = DateTime.UtcNow.AddMonths(1) },
            new() { Code = "EXPIRED20", Percentage = 20m,  ExpiresAtUtc = DateTime.UtcNow.AddDays(-5) },
            new() { Code = "HALFOFF",   Percentage = 150m, ExpiresAtUtc = DateTime.UtcNow.AddMonths(1) }
        };

        db.Customers.Add(customer);
        db.Products.AddRange(products);
        db.DiscountCodes.AddRange(discounts);
        await db.SaveChangesAsync();
    }
}
