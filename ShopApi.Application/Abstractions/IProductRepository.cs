using ShopApi.Domain.Entities;

namespace ShopApi.Application.Abstractions;

public interface IProductRepository
{
    Task<List<Product>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken ct = default);
}
