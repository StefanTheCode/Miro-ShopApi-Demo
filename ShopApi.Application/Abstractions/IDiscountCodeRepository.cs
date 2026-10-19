using ShopApi.Domain.Entities;

namespace ShopApi.Application.Abstractions;

public interface IDiscountCodeRepository
{
    Task<DiscountCode?> GetByCodeAsync(string code, CancellationToken ct = default);
}
