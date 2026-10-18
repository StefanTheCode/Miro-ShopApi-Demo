using Microsoft.AspNetCore.Mvc;
using ShopApi.Application.Orders.CreateOrder;

namespace ShopApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly CreateOrderHandler _createOrder;

    public OrdersController(CreateOrderHandler createOrder)
    {
        _createOrder = createOrder;
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

    /// <summary>Placeholder read endpoint so CreatedAtAction can resolve a location.</summary>
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id) => Ok(new { id });
}
