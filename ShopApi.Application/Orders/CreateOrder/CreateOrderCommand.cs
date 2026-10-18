namespace ShopApi.Application.Orders.CreateOrder;

public record CreateOrderItemDto(int ProductId, int Quantity);

public record CreateOrderCommand(
    int CustomerId,
    IReadOnlyList<CreateOrderItemDto> Items,
    string? DiscountCode = null);

public record CreateOrderResult(int OrderId, decimal Total);
