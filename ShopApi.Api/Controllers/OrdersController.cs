using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi.Application.Orders.CreateOrder;
using ShopApi.Infrastructure.Persistence;

namespace ShopApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly CreateOrderHandler _createOrder;
    private readonly AppDbContext _db;

    public OrdersController(CreateOrderHandler createOrder, AppDbContext db)
    {
        _createOrder = createOrder;
        _db = db;
    }

    /// <summary>Create a new order. Controller -> Handler -> Repositories -> DbContext -> DB.</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command, CancellationToken ct)
    {
        try
        {
            var result = await _createOrder.HandleAsync(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.OrderId }, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return UnprocessableEntity(new { error = ex.Message });
        }
    }

    /// <summary>List orders with a quick summary.</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var orders = await _db.Orders.AsNoTracking().ToListAsync(ct);

        var result = new List<object>();
        foreach (var order in orders)
        {
            // load this order's items + their products
            var items = await _db.OrderItems
                .Where(i => i.OrderId == order.Id)
                .ToListAsync(ct);

            var itemCount = items.Sum(i => i.Quantity);
            result.Add(new { order.Id, order.Status, order.DiscountPercentage, itemCount });
        }

        return Ok(result);
    }

    /// <summary>Placeholder read endpoint so CreatedAtAction can resolve a location.</summary>
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id) => Ok(new { id });
}
