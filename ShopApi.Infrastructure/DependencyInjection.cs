using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopApi.Application.Abstractions;
using ShopApi.Application.Orders.CreateOrder;
using ShopApi.Infrastructure.Persistence;
using ShopApi.Infrastructure.Persistence.Repositories;

namespace ShopApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IDiscountCodeRepository, DiscountCodeRepository>();

        // Application use-case handler
        services.AddScoped<CreateOrderHandler>();

        return services;
    }
}
