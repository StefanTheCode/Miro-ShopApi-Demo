using Microsoft.EntityFrameworkCore;
using ShopApi.Application.Abstractions;
using ShopApi.Domain.Entities;

namespace ShopApi.Infrastructure.Persistence.Repositories;

public class DiscountCodeRepository : IDiscountCodeRepository
{
    private readonly AppDbContext _db;

    public DiscountCodeRepository(AppDbContext db) => _db = db;

    public async Task<DiscountCode?> GetByCodeAsync(string code, CancellationToken ct = default)
        => await _db.DiscountCodes.FirstOrDefaultAsync(d => d.Code == code, ct);
}
