using ShopApi.Application.Abstractions;
using ShopApi.Domain.Entities;

namespace ShopApi.Application.Orders.CreateOrder;

/// <summary>
/// Handles the "Create Order" use case:
/// validates the request, loads the products, applies a discount code (new), and persists it.
/// Controller -> Handler -> Repositories -> DbContext -> DB
/// </summary>
public class CreateOrderHandler
{
    private readonly IOrderRepository _orders;
    private readonly IProductRepository _products;
    private readonly IDiscountCodeRepository _discounts;

    public CreateOrderHandler(
        IOrderRepository orders,
        IProductRepository products,
        IDiscountCodeRepository discounts)
    {
        _orders = orders;
        _products = products;
        _discounts = discounts;
    }

    public async Task<CreateOrderResult> HandleAsync(CreateOrderCommand command, CancellationToken ct = default)
    {
        if (command.Items is null || command.Items.Count == 0)
            throw new ArgumentException("An order must contain at least one item.");

        var productIds = command.Items.Select(i => i.ProductId).Distinct().ToList();
        var products = await _products.GetByIdsAsync(productIds, ct);

        if (products.Count != productIds.Count)
            throw new InvalidOperationException("One or more products do not exist.");

        var order = new Order
        {
            CustomerId = command.CustomerId,
            Status = OrderStatus.Pending,
            Items = command.Items.Select(i =>
            {
                var product = products.First(p => p.Id == i.ProductId);
                return new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = i.Quantity,
                    UnitPrice = product.Price
                };
            }).ToList()
        };

        // NEW: apply a discount code if one was supplied
        if (!string.IsNullOrWhiteSpace(command.DiscountCode))
        {
            var discount = await _discounts.GetByCodeAsync(command.DiscountCode, ct);
            if (discount is not null)
            {
                order.DiscountPercentage = discount.Percentage;
            }
        }

        await _orders.AddAsync(order, ct);
        await _orders.SaveChangesAsync(ct);

        return new CreateOrderResult(order.Id, order.Total);
    }
}
