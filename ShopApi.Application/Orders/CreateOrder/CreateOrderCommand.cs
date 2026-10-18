namespace ShopApi.Application.Orders.CreateOrder;

public record CreateOrderItemDto(int ProductId, int Quantity);

public record CreateOrderCommand(int CustomerId, IReadOnlyList<CreateOrderItemDto> Items);

public record CreateOrderResult(int OrderId, decimal Total);
