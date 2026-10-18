using Microsoft.EntityFrameworkCore;
using ShopApi.Application.Abstractions;
using ShopApi.Domain.Entities;

namespace ShopApi.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext db) => _db = db;

    public async Task<List<Product>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken ct = default)
        => await _db.Products
            .Where(p => ids.Contains(p.Id))
            .ToListAsync(ct);
}
